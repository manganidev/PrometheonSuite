namespace  PrometheonSuite.Identity.UseCases.Roles.List;

public record ListRolesQuery(
  int? Page = 1,
  int? PerPage = Constants.DEFAULT_PAGE_SIZE
) : IQuery<Result<PagedResult<RoleDto>>>;
