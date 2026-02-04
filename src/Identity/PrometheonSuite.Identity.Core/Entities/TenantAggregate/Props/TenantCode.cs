using Vogen;

namespace PrometheonSuite.Identity.Entities.TenantAggregate;

[ValueObject<string>]
public readonly partial struct TenantCode
{
  public static int MaxLength => 50;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Tenant code cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Tenant code cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
