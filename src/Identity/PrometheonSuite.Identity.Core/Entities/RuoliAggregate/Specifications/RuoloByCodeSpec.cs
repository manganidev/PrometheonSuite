namespace PrometheonSuite.Identity.Entities.RuoloAggregate.Specifications;

public class RuoloByCodeSpec : Specification<Ruolo>
{
  public RuoloByCodeSpec(RuoloCode code)
  {
    Query
      .Where(r => r.Code == code);
  }
}
