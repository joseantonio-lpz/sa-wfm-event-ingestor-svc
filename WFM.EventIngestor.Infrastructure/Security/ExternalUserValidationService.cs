using System.Net.Http.Json;
using WFM.EventIngestor.Application.Interfaces;
using WFM.EventIngestor.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace WFM.EventIngestor.Infrastructure.Security
{
    public class ExternalUserValidationService : IUserValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationSettings _settings;

        public ExternalUserValidationService(HttpClient httpClient, IOptions<AuthenticationSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        /// <summary>
        /// Valida las credenciales de un usuario contra un servicio externo.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var request = new
            {
                Username = username,
                Password = password,
                system_info = _settings.Source
            };
            var response = await _httpClient.PostAsJsonAsync("oauthUser/validate", request);
            if (response.IsSuccessStatusCode)
            {
                return true; // Si la respuesta es exitosa, se asume que la validación fue exitosa
            }
            return false; // Si la respuesta no es exitosa, se asume que la validación falló
        }
    }
}