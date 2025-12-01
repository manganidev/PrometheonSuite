using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.Roles.Create;

public record CreateRoleCommand(
  RoleCode Code,
  RoleName Name,
  string? Description = null
) : ICommand<Result<RoleId>>;
