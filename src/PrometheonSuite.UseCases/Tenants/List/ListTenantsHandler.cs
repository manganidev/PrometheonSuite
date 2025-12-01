using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.Tenants.List;

public class ListTenantsHandler(IRepository<Tenant> repository)
  : IQueryHandler<ListTenantsQuery, Result<PagedResult<TenantDto>>>
{
  private readonly IRepository<Tenant> _repository = repository;

  public async ValueTask<Result<PagedResult<TenantDto>>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;

    if (page < 1)
    {
      return Result<PagedResult<TenantDto>>.Error("Page must be >= 1");
    }

    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE)
    {
      return Result<PagedResult<TenantDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");
    }

    var totalCount = await _repository.CountAsync(cancellationToken);
    var skip = (page - 1) * perPage;

    var tenants = await _repository.ListAsync(cancellationToken);
    var pagedTenants = tenants
      .OrderBy(t => t.Name.Value)
      .Skip(skip)
      .Take(perPage)
      .ToList();

    var dtos = pagedTenants
      .Select(t => new TenantDto(t.Id, t.Name, t.Code, t.IsActive))
      .ToList();

    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    var result = new PagedResult<TenantDto>(dtos, page, perPage, totalCount, totalPages);

    return Result<PagedResult<TenantDto>>.Success(result);
  }
}
