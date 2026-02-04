using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.AddRole;

public record AddRoleToUserTenantCommand(
  UserTenantId UserTenantId,
  RoleId RoleId
) : ICommand<Result<UserTenantDto>>;
