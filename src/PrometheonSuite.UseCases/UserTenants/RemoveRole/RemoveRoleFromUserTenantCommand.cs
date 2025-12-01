using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.RemoveRole;

public record RemoveRoleFromUserTenantCommand(
  UserTenantId UserTenantId,
  RoleId RoleId
) : ICommand<Result<UserTenantDto>>;
