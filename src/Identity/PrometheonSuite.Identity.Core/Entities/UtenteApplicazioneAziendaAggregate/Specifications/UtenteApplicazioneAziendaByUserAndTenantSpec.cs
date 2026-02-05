
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate.Specifications;

public class UtenteApplicazioneAziendaByUserAndAziendaSpec : Specification<UtenteApplicazioneAzienda>
{
  public UtenteApplicazioneAziendaByUserAndAziendaSpec(UtenteId userId, AziendaId tenantId)
  {
    Query
      .Where(ut => ut.UserId == userId && ut.AziendaId == tenantId)
      .Include(ut => ut.UtenteApplicazioneAziendaFiguras);
  }
}
