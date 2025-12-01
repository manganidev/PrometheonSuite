using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures.List;

public class ListFiguresHandler(IRepository<Figure> repository)
  : IQueryHandler<ListFiguresQuery, Result<PagedResult<FigureDto>>>
{
  private readonly IRepository<Figure> _repository = repository;

  public async ValueTask<Result<PagedResult<FigureDto>>> Handle(ListFiguresQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;

    if (page < 1)
    {
      return Result<PagedResult<FigureDto>>.Error("Page must be >= 1");
    }

    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE)
    {
      return Result<PagedResult<FigureDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");
    }

    var totalCount = await _repository.CountAsync(cancellationToken);
    var skip = (page - 1) * perPage;

    var figures = await _repository.ListAsync(cancellationToken);
    var pagedFigures = figures
      .OrderBy(f => f.Code.Value)
      .Skip(skip)
      .Take(perPage)
      .ToList();

    var dtos = pagedFigures
      .Select(f => new FigureDto(
        f.Id, 
        f.Code, 
        f.Name, 
        f.Description, 
        f.FigureRoles.Select(fr => fr.RoleId.Value).ToList()))
      .ToList();

    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    var result = new PagedResult<FigureDto>(dtos, page, perPage, totalCount, totalPages);

    return Result<PagedResult<FigureDto>>.Success(result);
  }
}
