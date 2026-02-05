namespace PrometheonSuite.Identity.Entities.RuoloAggregate.Specifications;

public class RuoloByIdSpec : Specification<Ruolo>
{
  public RuoloByIdSpec(RuoloId roleId)
  {
    Query
      .Where(r => r.Id == roleId);
  }
}
