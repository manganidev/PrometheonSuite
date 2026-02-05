using Vogen;

namespace PrometheonSuite.Identity.Entities.AziendaAggregate;

[ValueObject<string>]
public readonly partial struct AziendaName
{
  public static int MaxLength => 200;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Azienda name cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Azienda name cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
