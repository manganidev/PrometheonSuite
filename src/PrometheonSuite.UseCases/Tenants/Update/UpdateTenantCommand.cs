using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.Tenants.Update;

public record UpdateTenantCommand(
  TenantId TenantId,
  TenantName Name,
  TenantCode Code
) : ICommand<Result<TenantDto>>;
