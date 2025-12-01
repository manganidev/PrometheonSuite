using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Vogen;

namespace PrometheonSuite.Identity.Entities.UtenteAggregate;

[ValueObject<string>]
public readonly partial struct Email
{
  public const int MaxLength = 256;

  private static readonly Regex _regex =
      new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

  private static Validation Validate(string value) =>
      string.IsNullOrWhiteSpace(value) || !_regex.IsMatch(value)
          ? Validation.Invalid("Email is not valid.")
          : value.Length > MaxLength
              ? Validation.Invalid($"Email cannot exceed {MaxLength} characters.")
              : Validation.Ok;
}
