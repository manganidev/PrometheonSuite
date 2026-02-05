namespace PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;

public class AziendaByIdSpec : Specification<Azienda>
{
  public AziendaByIdSpec(AziendaId tenantId)
  {
    Query
      .Where(t => t.Id == tenantId);
  }
}
