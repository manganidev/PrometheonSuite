using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.Tenants.Get;

public record GetTenantQuery(TenantId TenantId) : IQuery<Result<TenantDto>>;
