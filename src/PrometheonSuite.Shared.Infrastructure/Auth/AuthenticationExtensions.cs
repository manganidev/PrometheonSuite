

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PrometheonSuite.Shared.Infrastructure.Auth;

public static class AuthenticationExtensions
{
  public static IServiceCollection AddJwtAuthentication(
      this IServiceCollection services, IConfiguration configuration)
  {
    var key = configuration["Auth:Key"]!;
    var issuer = configuration["Auth:Issuer"];
    var audience = configuration["Auth:Audience"];

    services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(key)
              ),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
          };
        });

    return services;
  }

}

