using Vogen;

namespace PrometheonSuite.Identity.Entities.RuoloAggregate;

[ValueObject<string>]
public readonly partial struct RuoloName
{
  public static int MaxLength => 200;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Ruolo name cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Ruolo name cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
