using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.Tenants.Create;

public record CreateTenantCommand(
  TenantName Name,
  TenantCode Code
) : ICommand<Result<TenantId>>;
