using PrometheonSuite.Identity.Entities.UserTenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.Get;

public record GetUserTenantQuery(UserTenantId UserTenantId) : IQuery<Result<UserTenantDto>>;
