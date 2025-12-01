using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.Roles.Get;

public record GetRoleQuery(RoleId RoleId) : IQuery<Result<RoleDto>>;
