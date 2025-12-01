using Vogen;

namespace PrometheonSuite.Identity.Entities.FigureAggregate;

[ValueObject<string>]
public readonly partial struct FigureName
{
  public static int MaxLength => 200;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Figure name cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Figure name cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
