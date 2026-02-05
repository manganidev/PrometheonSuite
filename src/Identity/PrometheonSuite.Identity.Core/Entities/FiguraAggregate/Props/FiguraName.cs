using Vogen;

namespace PrometheonSuite.Identity.Entities.FiguraAggregate;

[ValueObject<string>]
public readonly partial struct FiguraName
{
  public static int MaxLength => 200;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Figura name cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Figura name cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
