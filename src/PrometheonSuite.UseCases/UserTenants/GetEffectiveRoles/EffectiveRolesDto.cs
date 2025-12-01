using  PrometheonSuite.Identity.UseCases.Roles;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.GetEffectiveRoles;

public record EffectiveRolesDto(
  IReadOnlyList<RoleDto> AllRoles,
  IReadOnlyList<RoleDto> RolesFromFigures,
  IReadOnlyList<RoleDto> DirectRoles
);
