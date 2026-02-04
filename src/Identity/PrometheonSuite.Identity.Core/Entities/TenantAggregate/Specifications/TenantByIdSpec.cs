namespace PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;

public class TenantByIdSpec : Specification<Tenant>
{
  public TenantByIdSpec(TenantId tenantId)
  {
    Query
      .Where(t => t.Id == tenantId);
  }
}
