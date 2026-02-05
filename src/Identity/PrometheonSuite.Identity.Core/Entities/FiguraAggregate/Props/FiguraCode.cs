using Vogen;

namespace PrometheonSuite.Identity.Entities.FiguraAggregate;

[ValueObject<string>]
public readonly partial struct FiguraCode
{
  public static int MaxLength => 100;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Figura code cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Figura code cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
