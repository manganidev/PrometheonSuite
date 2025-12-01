using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.Tenants;

public record TenantDto(
  TenantId Id,
  TenantName Name,
  TenantCode Code,
  bool IsActive
);
