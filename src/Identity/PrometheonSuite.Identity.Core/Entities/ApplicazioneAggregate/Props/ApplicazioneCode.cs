using Vogen;

namespace PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

[ValueObject<string>]
public readonly partial struct ApplicazioneCode
{
  public static int MaxLength => 50;

  private static Validation Validate(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Validation.Invalid("Applicazione code cannot be empty.");
    }

    if (value.Length > MaxLength)
    {
      return Validation.Invalid($" Applicazione code cannot be longer than {MaxLength} characters.");
    }

    return Validation.Ok;
  }
}
