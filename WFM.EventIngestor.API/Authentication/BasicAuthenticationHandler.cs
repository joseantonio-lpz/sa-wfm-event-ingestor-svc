using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using WFM.EventIngestor.Application.Interfaces;

namespace WFM.EventIngestor.API.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserValidationService _userValidationService;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IUserValidationService userValidationService)
            : base(options, logger, encoder)
        {
            _userValidationService = userValidationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }
            try
            {
                var authorizationHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(authorizationHeader))
                {
                    return AuthenticateResult.Fail("Missing Authorization Header");
                }

                var authHeader = AuthenticationHeaderValue.Parse(authorizationHeader);
                if (authHeader.Scheme != "Basic")
                {
                    return AuthenticateResult.Fail("Invalid Authorization Scheme");
                }

                if (string.IsNullOrEmpty(authHeader.Parameter))
                {
                    return AuthenticateResult.Fail("Invalid Authorization Credentials");
                }

                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                if (credentials.Length != 2)
                {
                    return AuthenticateResult.Fail("Invalid Authorization Credentials");
                }

                var username = credentials[0];
                var password = credentials[1];
                
                if (await _userValidationService.ValidateUserAsync(username, password))
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
                return AuthenticateResult.Fail("Invalid username or password.");
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }            
        }
    }    
}