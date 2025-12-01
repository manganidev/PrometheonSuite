using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Roles.Delete;

public class DeleteRoleHandler(IRepository<Role> repository)
  : ICommandHandler<DeleteRoleCommand, Result>
{
  private readonly IRepository<Role> _repository = repository;

  public async ValueTask<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
  {
    var role = await _repository.FirstOrDefaultAsync(
      new RoleByIdSpec(request.RoleId), 
      cancellationToken);

    if (role == null)
    {
      return Result.NotFound();
    }

    // Hard delete for roles (or implement soft delete if needed)
    await _repository.DeleteAsync(role, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
