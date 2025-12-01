namespace PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;

public class RoleByCodeSpec : Specification<Role>
{
  public RoleByCodeSpec(RoleCode code)
  {
    Query
      .Where(r => r.Code == code);
  }
}
