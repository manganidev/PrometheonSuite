using System;
using System.Collections.Generic;
using System.Text;
using Vogen;

namespace PrometheonSuite.Identity.Entities.UtenteAggregate;

[ValueObject<string>]
public readonly partial struct Username
{
  public const int MaxLength = 150;

  private static Validation Validate(string value) =>
      string.IsNullOrWhiteSpace(value)
          ? Validation.Invalid("Username non può essere vuoto.")
          : value.Length > MaxLength
              ? Validation.Invalid($"Username non può superare {MaxLength} caratteri.")
              : Validation.Ok;
}
