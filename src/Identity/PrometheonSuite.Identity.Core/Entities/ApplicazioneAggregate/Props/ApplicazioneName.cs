using Vogen;

namespace PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

[ValueObject<string>]
public readonly partial struct ApplicazioneName
{
  public static int MaxLength => 200;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Applicazione name cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($"Applicazione name cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
