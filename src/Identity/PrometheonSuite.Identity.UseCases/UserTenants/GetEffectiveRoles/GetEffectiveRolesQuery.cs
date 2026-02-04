using PrometheonSuite.Identity.Entities.UserTenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.GetEffectiveRoles;

public record GetEffectiveRolesQuery(UserTenantId UserTenantId) 
  : IQuery<Result<EffectiveRolesDto>>;
