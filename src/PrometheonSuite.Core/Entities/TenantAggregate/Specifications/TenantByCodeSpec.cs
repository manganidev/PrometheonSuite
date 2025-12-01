namespace PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;

public class TenantByCodeSpec : Specification<Tenant>
{
  public TenantByCodeSpec(TenantCode code)
  {
    Query
      .Where(t => t.Code == code);
  }
}
