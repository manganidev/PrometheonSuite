
using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace PrometheonSuite.Identity.Entities.UserTenantAggregate;

public class UserTenantRole
{
  public UserTenantId UserTenantId { get; private set; }
  public RoleId RoleId { get; private set; }

  public UserTenantRole(UserTenantId userTenantId, RoleId roleId)
  {
    UserTenantId = userTenantId;
    RoleId = roleId;
  }

#pragma warning disable CS8618
  private UserTenantRole()
  {
  }
#pragma warning restore CS8618
}
