using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Roles.Create;

public class CreateRoleHandler(ICoreRepository<Role> repository)
  : ICommandHandler<CreateRoleCommand, Result<RoleId>>
{
  private readonly ICoreRepository<Role> _repository = repository;

  public async ValueTask<Result<RoleId>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
  {
    // Check if role code already exists
    var existingRole = await _repository.FirstOrDefaultAsync(
      new RoleByCodeSpec(request.Code), 
      cancellationToken);

    if (existingRole != null)
    {
      return Result<RoleId>.Error($"A role with code '{request.Code.Value}' already exists.");
    }

    var role = new Role(request.Code, request.Name, request.Description);
    
    var createdRole = await _repository.AddAsync(role, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<RoleId>.Success(createdRole.Id);
  }
}
