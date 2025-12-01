using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.Roles.Delete;

public record DeleteRoleCommand(RoleId RoleId) : ICommand<Result>;
