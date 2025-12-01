using Vogen;

namespace PrometheonSuite.Identity.Entities.FigureAggregate;

[ValueObject<string>]
public readonly partial struct FigureCode
{
  public static int MaxLength => 100;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Figure code cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Figure code cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
