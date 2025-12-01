using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.Tenants.Delete;

public record DeleteTenantCommand(TenantId TenantId) : ICommand<Result>;
