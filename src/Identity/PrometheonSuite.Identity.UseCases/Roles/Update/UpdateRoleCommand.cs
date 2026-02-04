using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.Roles.Update;

public record UpdateRoleCommand(
  RoleId RoleId,
  RoleCode Code,
  RoleName Name,
  string? Description
) : ICommand<Result<RoleDto>>;
