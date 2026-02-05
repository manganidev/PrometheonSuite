using Vogen;

namespace PrometheonSuite.Identity.Entities.AziendaAggregate;

[ValueObject<string>]
public readonly partial struct AziendaCode
{
  public static int MaxLength => 50;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Azienda code cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Azienda code cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
