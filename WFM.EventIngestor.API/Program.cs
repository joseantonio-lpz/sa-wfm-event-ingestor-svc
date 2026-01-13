using WFM.EventIngestor.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using WFM.EventIngestor.API.Authentication;
using Microsoft.AspNetCore.Mvc;
using WFM.EventIngestor.Infrastructure.Security;
using WFM.EventIngestor.Application.Interfaces;
using Microsoft.Extensions.Options;
using WFM.EventIngestor.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.AllowInputFormatterExceptionMessages = false;
    });
    
// Configurar la respuesta de error para modelos inválidos
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Errors = e.Value?.Errors.Select(x => x.ErrorMessage)
            });
        var response = new
        {
            Title = "Errores de validación",
            Status = StatusCodes.Status400BadRequest,
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "wfm.event.ingestor", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Autenticación básica con username:password codificado en base64"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });
});
// Configurar la autenticación
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

// Configurar los ajustes de autenticación desde appsettings.json
builder.Services.Configure<AuthenticationSettings>(
    builder.Configuration.GetSection("AuthenticationSettings"));
builder.Services.Configure<CustomLoggingSettings>(
    builder.Configuration.GetSection("Logging:CustomSettings"));
builder.Services.Configure<NLogSettings>(
    builder.Configuration.GetSection("Logging:NLog"));
builder.Services.Configure<KafkaSettings>(
    builder.Configuration.GetSection("KafkaSettings"));

// Configurar el cliente HTTP para el servicio de validación de usuario
builder.Services.AddHttpClient<IUserValidationService, ExternalUserValidationService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<AuthenticationSettings>>().Value;
    client.BaseAddress = new Uri(settings.UrlValidateUser);
    client.DefaultRequestHeaders.Add("Authorization", $"Basic {settings.Basic}");
});

// Agregar health checks
builder.Services.AddHealthChecks()
    .AddOracle(
        connectionString: configuration.GetConnectionString("OracleDb")
                        ?? throw new InvalidOperationException("La cadena de conexión 'OracleDb' no está configurada."),
        name: "oracle",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "oracle" }
    )
    .AddCheck("self", () => HealthCheckResult.Healthy("Servidor en ejecución"));

// Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Otros servicios
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "wfm.event.ingestor");
    c.RoutePrefix = string.Empty; // Swagger en la raíz
});
//app.UseHttpsRedirection();
app.UseRouting(); // Importante para el enrutamiento
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.MapControllers(); // Mapea los controladores
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            }),
            duration = report.TotalDuration
        };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}).AllowAnonymous();
app.Run();