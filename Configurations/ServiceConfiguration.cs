using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using Microsoft.OpenApi.Models;
using UsingFirebase.Services;

namespace UsingFirebase.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        var firebaseValidIssuer = Environment.GetEnvironmentVariable("FIREBASE_VALID_ISSUER");
        var firebaseAudience = Environment.GetEnvironmentVariable("FIREBASE_AUDIENCE");
        var firebaseCredentials = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIALS");

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromJson(firebaseCredentials)
        });

        services.AddHttpClient<string>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddScoped<AuthService>();
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor, insira o token JWT com o prefixo 'Bearer '",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
}