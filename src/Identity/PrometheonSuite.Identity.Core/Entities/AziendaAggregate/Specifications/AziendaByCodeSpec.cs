namespace PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;

public class AziendaByCodeSpec : Specification<Azienda>
{
  public AziendaByCodeSpec(AziendaCode code)
  {
    Query
      .Where(t => t.Code == code);
  }
}
