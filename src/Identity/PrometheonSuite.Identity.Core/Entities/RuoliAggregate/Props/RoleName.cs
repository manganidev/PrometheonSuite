using Vogen;

namespace PrometheonSuite.Identity.Entities.RoleAggregate;

[ValueObject<string>]
public readonly partial struct RoleName
{
  public static int MaxLength => 200;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Role name cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Role name cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
