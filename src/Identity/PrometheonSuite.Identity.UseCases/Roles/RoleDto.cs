using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.Roles;

public record RoleDto(
  RoleId Id,
  RoleCode Code,
  RoleName Name,
  string? Description
);
