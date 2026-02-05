
namespace PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate.Specifications;

public class AziendaApplicazioneByIdSpec : Specification<AziendaApplicazione>
{
  public AziendaApplicazioneByIdSpec(AziendaApplicazioneId aziendaApplicazioneId)
  {
    Query
      .Where(ut => ut.Id == aziendaApplicazioneId);
  }
}
