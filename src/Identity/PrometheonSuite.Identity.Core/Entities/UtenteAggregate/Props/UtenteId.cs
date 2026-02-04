using Vogen;

namespace PrometheonSuite.Identity.Entities.UtenteAggregate;

[ValueObject<Guid>]
public readonly partial struct UtenteId
{
  // EF Core needs to be able to create instances with Guid.Empty temporarily
  // The database will generate the actual ID value
  private static Validation Validate(Guid value) => Validation.Ok;
}
