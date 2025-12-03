using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using global::PrometheonSuite.Identity.Core.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Infrastructure.Data;
using PrometheonSuite.Identity.Core.Entities.TokenAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Auth;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly CoreDbContext _db;

  public TokenService(IConfiguration config, CoreDbContext db)
  {
    _config = config;
    _db = db;
  }

  public async Task<AuthResult> GenerateTokensAsync(Utente user, CancellationToken ct = default)
  {
    // 1. Crea access token
    var accessToken = CreateAccessToken(user, out DateTimeOffset expiresAt);

    // 2. Crea refresh token
    var refreshToken = GenerateRefreshToken();

    var refreshEntity = new RefreshToken
    {
      Id = Guid.NewGuid(),
      Token = refreshToken,
      UserId = user.Id.Value,
      ExpiresAt = DateTimeOffset.UtcNow.AddDays(7),
      Used = false,
      Revoked = false
    };

    _db.RefreshTokens.Add(refreshEntity);
    await _db.SaveChangesAsync(ct);

    return new AuthResult
    {
      AccessToken = accessToken,
      RefreshToken = refreshToken,
      AccessTokenExpiresAt = expiresAt
    };
  }


  public async Task<AuthResult?> RefreshTokensAsync(string refreshToken, CancellationToken ct = default)
  {
    var stored = await _db.RefreshTokens
        .FirstOrDefaultAsync(x => x.Token == refreshToken, ct);

    if (stored is null || stored.Used || stored.Revoked || stored.ExpiresAt < DateTimeOffset.UtcNow)
      return null;

    // Recupera utente
    var user = await _db.Utenti.FirstOrDefaultAsync(u => u.Id == stored.UserId, ct);
    if (user is null) return null;

    // Mark as used
    stored.Used = true;
    await _db.SaveChangesAsync(ct);

    // Genera nuovi token
    return await GenerateTokensAsync(user, ct);
  }

  // ---------------------------------------
  // PRIVATE METHODS
  // ---------------------------------------

  private string CreateAccessToken(Utente user, out DateTimeOffset expiresAt)
  {
    var issuer = _config["Auth:Issuer"];
    var audience = _config["Auth:Audience"];
    var key = _config["Auth:Key"];

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    expiresAt = DateTimeOffset.UtcNow.AddMinutes(30);

    var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username.Value)
            // aggiungi altri claim applicativi qui se/quando disponibili
        };

    var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: expiresAt.UtcDateTime,
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  private string GenerateRefreshToken()
  {
    var bytes = RandomNumberGenerator.GetBytes(64);
    return Convert.ToBase64String(bytes);
  }
}
