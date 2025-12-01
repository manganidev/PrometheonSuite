
namespace PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;

public class UserTenantByIdSpec : Specification<UserTenant>
{
  public UserTenantByIdSpec(UserTenantId userTenantId)
  {
    Query
      .Where(ut => ut.Id == userTenantId)
      .Include(ut => ut.UserTenantFigures)
      .Include(ut => ut.UserTenantRoles);
  }
}
