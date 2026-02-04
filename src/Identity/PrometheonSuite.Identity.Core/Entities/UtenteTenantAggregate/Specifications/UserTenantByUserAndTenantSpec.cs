
using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;

public class UserTenantByUserAndTenantSpec : Specification<UserTenant>
{
  public UserTenantByUserAndTenantSpec(UtenteId userId, TenantId tenantId)
  {
    Query
      .Where(ut => ut.UserId == userId && ut.TenantId == tenantId)
      .Include(ut => ut.UserTenantFigures)
      .Include(ut => ut.UserTenantRoles);
  }
}
