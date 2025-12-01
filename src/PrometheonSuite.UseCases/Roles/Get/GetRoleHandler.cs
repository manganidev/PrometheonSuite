using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Roles.Get;

public class GetRoleHandler(ICoreRepository<Role> repository)
  : IQueryHandler<GetRoleQuery, Result<RoleDto>>
{
  private readonly ICoreRepository<Role> _repository = repository;

  public async ValueTask<Result<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
  {
    var role = await _repository.FirstOrDefaultAsync(
      new RoleByIdSpec(request.RoleId), 
      cancellationToken);

    if (role == null)
    {
      return Result<RoleDto>.NotFound();
    }

    var dto = new RoleDto(role.Id, role.Code, role.Name, role.Description);
    return Result<RoleDto>.Success(dto);
  }
}
