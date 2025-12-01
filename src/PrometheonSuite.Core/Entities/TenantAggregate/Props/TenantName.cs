using Vogen;

namespace PrometheonSuite.Identity.Entities.TenantAggregate;

[ValueObject<string>]
public readonly partial struct TenantName
{
  public static int MaxLength => 200;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Tenant name cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Tenant name cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
