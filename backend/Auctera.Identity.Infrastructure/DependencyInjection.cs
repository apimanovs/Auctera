using System.Text;

using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Infrastructure.Configuration;
using Auctera.Identity.Infrastructure.Hasher;
using Auctera.Identity.Infrastructure.Repository;
using Auctera.Identity.Infrastructure.Token;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auctera.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection(JwtOptions.SectionName);
        var jwtOptions = jwtSection.Get<JwtOptions>() ?? new JwtOptions();

        services
            .AddOptions<JwtOptions>()
            .Bind(jwtSection)
            .Validate(options => !string.IsNullOrWhiteSpace(options.Secret),
                "Jwt secret is required. Configure Jwt:Secret via UserSecrets or environment variable JWT__Secret")

            .Validate(options => !string.IsNullOrWhiteSpace(options.Issuer), "Jwt issuer is required")
            .Validate(options => !string.IsNullOrWhiteSpace(options.Audience), "Jwt audience is required")
            .Validate(options => options.ExpirationInMinutes > 0, "Jwt expiration must be greater than 0")
            .ValidateOnStart();

        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

        services.AddAuthorization();

        return services;
    }
}
