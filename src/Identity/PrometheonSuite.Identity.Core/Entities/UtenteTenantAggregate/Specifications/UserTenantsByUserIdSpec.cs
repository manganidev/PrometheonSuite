

using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;

public class UserTenantsByUserIdSpec : Specification<UserTenant>
{
  public UserTenantsByUserIdSpec(UtenteId userId)
  {
    Query
      .Where(ut => ut.UserId == userId)
      .Include(ut => ut.UserTenantFigures)
      .Include(ut => ut.UserTenantRoles);
  }
}
