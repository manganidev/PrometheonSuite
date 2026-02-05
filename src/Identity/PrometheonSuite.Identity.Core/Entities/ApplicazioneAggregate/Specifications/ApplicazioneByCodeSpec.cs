namespace PrometheonSuite.Identity.Entities.ApplicazioneAggregate.Specifications;

public class ApplicazioneByCodeSpec : Specification<Applicazione>
{
  public ApplicazioneByCodeSpec(ApplicazioneCode code)
  {
    Query
      .Where(t => t.Code == code);
  }
}
