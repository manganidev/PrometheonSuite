using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Roles.Update;

public class UpdateRoleHandler(ICoreRepository<Role> repository)
  : ICommandHandler<UpdateRoleCommand, Result<RoleDto>>
{
  private readonly ICoreRepository<Role> _repository = repository;

  public async ValueTask<Result<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
  {
    var role = await _repository.FirstOrDefaultAsync(
      new RoleByIdSpec(request.RoleId), 
      cancellationToken);

    if (role == null)
    {
      return Result<RoleDto>.NotFound();
    }

    // Check if new code already exists on another role
    if (role.Code != request.Code)
    {
      var existingRole = await _repository.FirstOrDefaultAsync(
        new RoleByCodeSpec(request.Code), 
        cancellationToken);

      if (existingRole != null && existingRole.Id != role.Id)
      {
        return Result<RoleDto>.Error($"A role with code '{request.Code.Value}' already exists.");
      }
    }

    role.AggiornaInfo(request.Code, request.Name, request.Description);
    await _repository.SaveChangesAsync(cancellationToken);

    var dto = new RoleDto(role.Id, role.Code, role.Name, role.Description);
    return Result<RoleDto>.Success(dto);
  }
}
