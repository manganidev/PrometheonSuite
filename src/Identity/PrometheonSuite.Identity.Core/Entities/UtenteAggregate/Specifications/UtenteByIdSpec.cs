
namespace PrometheonSuite.Identity.Entities.UtenteAggregate.Specifications;

public class UtenteByIdSpec : Specification<Utente>
{
  public UtenteByIdSpec(UtenteId utenteId) =>
    Query
        .Where(utente => utente.Id == utenteId);
}
