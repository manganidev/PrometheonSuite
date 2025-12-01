using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Roles.Get;

public class GetRoleHandler(IRepository<Role> repository)
  : IQueryHandler<GetRoleQuery, Result<RoleDto>>
{
  private readonly IRepository<Role> _repository = repository;

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
