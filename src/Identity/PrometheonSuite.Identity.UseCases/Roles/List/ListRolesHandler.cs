using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Roles.List;

public class ListRolesHandler(ICoreRepository<Role> repository)
  : IQueryHandler<ListRolesQuery, Result<PagedResult<RoleDto>>>
{
  private readonly ICoreRepository<Role> _repository = repository;

  public async ValueTask<Result<PagedResult<RoleDto>>> Handle(ListRolesQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;

    if (page < 1)
    {
      return Result<PagedResult<RoleDto>>.Error("Page must be >= 1");
    }

    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE)
    {
      return Result<PagedResult<RoleDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");
    }

    var totalCount = await _repository.CountAsync(cancellationToken);
    var skip = (page - 1) * perPage;

    var roles = await _repository.ListAsync(cancellationToken);
    var pagedRoles = roles
      .OrderBy(r => r.Code.Value)
      .Skip(skip)
      .Take(perPage)
      .ToList();

    var dtos = pagedRoles
      .Select(r => new RoleDto(r.Id, r.Code, r.Name, r.Description))
      .ToList();

    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    var result = new PagedResult<RoleDto>(dtos, page, perPage, totalCount, totalPages);

    return Result<PagedResult<RoleDto>>.Success(result);
  }
}
