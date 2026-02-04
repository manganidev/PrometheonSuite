namespace  PrometheonSuite.Identity.UseCases.Tenants.List;

public record ListTenantsQuery(
  int? Page = 1,
  int? PerPage = Constants.DEFAULT_PAGE_SIZE
) : IQuery<Result<PagedResult<TenantDto>>>;
