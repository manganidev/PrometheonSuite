using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Core.Interfaces;

public interface ITokenService
{
  Task<AuthResult> GenerateTokensAsync(Utente user, CancellationToken ct = default);
  Task<AuthResult?> RefreshTokensAsync(string refreshToken, CancellationToken ct = default);
}

public sealed class AuthResult
{
  public string AccessToken { get; init; } = default!;
  public string RefreshToken { get; init; } = default!;
  public DateTimeOffset AccessTokenExpiresAt { get; init; }
}
