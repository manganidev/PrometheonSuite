using System;
using System.Collections.Generic;
using System.Text;

namespace PrometheonSuite.Identity.Core.Entities.TokenAggregate;


public class RefreshToken
{
  public Guid Id { get; set; }
  public string Token { get; set; } = default!;
  public Guid UserId { get; set; }
  public DateTimeOffset ExpiresAt { get; set; }
  public bool Used { get; set; }
  public bool Revoked { get; set; }
}
