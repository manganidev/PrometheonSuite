namespace PrometheonSuite.Identity.Entities.ApplicazioneAggregate.Specifications;

public class ApplicazioneByIdSpec : Specification<Applicazione>
{
  public ApplicazioneByIdSpec(ApplicazioneId tenantId)
  {
    Query
      .Where(t => t.Id == tenantId);
  }
}
