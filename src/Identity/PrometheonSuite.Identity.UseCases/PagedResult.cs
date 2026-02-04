namespace PrometheonSuite.Identity.UseCases;

public record PagedResult<T>(
  IReadOnlyList<T> Items,
  int Page,
  int PerPage,
  int TotalCount,
  int TotalPages);
