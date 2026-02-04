using Vogen;

namespace PrometheonSuite.Identity.Entities.RoleAggregate;

[ValueObject<string>]
public readonly partial struct RoleCode
{
  public static int MaxLength => 100;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Role code cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Role code cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
