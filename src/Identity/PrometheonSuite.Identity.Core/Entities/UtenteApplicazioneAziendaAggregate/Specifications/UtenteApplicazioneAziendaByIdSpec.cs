
namespace PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate.Specifications;

public class UtenteApplicazioneAziendaByIdSpec : Specification<UtenteApplicazioneAzienda>
{
  public UtenteApplicazioneAziendaByIdSpec(UtenteApplicazioneAziendaId userTenantId)
  {
    Query
      .Where(ut => ut.Id == userTenantId)
      .Include(ut => ut.UtenteApplicazioneAziendaFiguras);
  }
}
