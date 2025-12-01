using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants;

public record UserTenantDto(
  UserTenantId Id,
  UtenteId UserId,
  TenantId TenantId,
  bool IsActive,
  string? DefaultLocale,
  IReadOnlyList<Guid> FigureIds,
  IReadOnlyList<Guid> DirectRoleIds
);
