using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Figuras.List;

public class ListFigurasHandler(ICoreRepository<Figura> repository)
  : IQueryHandler<ListFigurasQuery, Result<PagedResult<FiguraDto>>>
{
  private readonly ICoreRepository<Figura> _repository = repository;

  public async ValueTask<Result<PagedResult<FiguraDto>>> Handle(ListFigurasQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;

    if (page < 1)
    {
      return Result<PagedResult<FiguraDto>>.Error("Page must be >= 1");
    }

    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE)
    {
      return Result<PagedResult<FiguraDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");
    }

    var totalCount = await _repository.CountAsync(cancellationToken);
    var skip = (page - 1) * perPage;

    var figures = await _repository.ListAsync(cancellationToken);
    var pagedFiguras = figures
      .OrderBy(f => f.Code.Value)
      .Skip(skip)
      .Take(perPage)
      .ToList();

    var dtos = pagedFiguras
      .Select(f => new FiguraDto(
        f.Id, 
        f.Code, 
        f.Name, 
        f.ApplicazioneId,
        f.Description, 
        f.FiguraRuolos.Select(fr => fr.RuoloId.Value).ToList()))
      .ToList();

    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    var result = new PagedResult<FiguraDto>(dtos, page, perPage, totalCount, totalPages);

    return Result<PagedResult<FiguraDto>>.Success(result);
  }
}
