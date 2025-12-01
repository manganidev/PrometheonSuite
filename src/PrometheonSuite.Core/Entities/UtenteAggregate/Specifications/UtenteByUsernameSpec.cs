using Ardalis.Specification;

namespace PrometheonSuite.Identity.Entities.UtenteAggregate.Specifications;

public sealed class UtenteByUsernameSpec : Specification<Utente>, ISingleResultSpecification<Utente>
{
  public UtenteByUsernameSpec(Username username)
  {
    Query.Where(x => x.Username == username);
  }
}
