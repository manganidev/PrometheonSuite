

using Microsoft.IdentityModel.Tokens;

namespace PrometheonSuite.Shared.Infrastructure.Auth;

public static class AuthenticationExtensions
{
  public static IServiceCollection AddJwtAuthentication(
      this IServiceCollection services, IConfiguration configuration)
  {
    services
        .AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
          options.Authority = configuration["Auth:Authority"];
          options.RequireHttpsMetadata = true;

          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidAudience = configuration["Auth:Audience"]
          };
        });

    return services;
  }
}

