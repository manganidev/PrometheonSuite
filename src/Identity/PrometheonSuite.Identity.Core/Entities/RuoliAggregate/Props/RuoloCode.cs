using Vogen;

namespace PrometheonSuite.Identity.Entities.RuoloAggregate;

[ValueObject<string>]
public readonly partial struct RuoloCode
{
  public static int MaxLength => 100;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Ruolo code cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Ruolo code cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
