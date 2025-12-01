namespace PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;

public class RoleByIdSpec : Specification<Role>
{
  public RoleByIdSpec(RoleId roleId)
  {
    Query
      .Where(r => r.Id == roleId);
  }
}
