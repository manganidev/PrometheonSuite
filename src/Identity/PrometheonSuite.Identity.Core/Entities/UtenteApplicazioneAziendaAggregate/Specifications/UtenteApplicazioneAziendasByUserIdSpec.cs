

using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate.Specifications;

public class UtenteApplicazioneAziendasByUserIdSpec : Specification<UtenteApplicazioneAzienda>
{
  public UtenteApplicazioneAziendasByUserIdSpec(UtenteId userId)
  {
    Query
      .Where(ut => ut.UserId == userId)
      .Include(ut => ut.UtenteApplicazioneAziendaFiguras);
  }
}
