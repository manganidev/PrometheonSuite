using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Aziendas.List;

public class ListAziendasHandler(ICoreRepository<Azienda> repository)
  : IQueryHandler<ListAziendasQuery, Result<PagedResult<AziendaDto>>>
{
  private readonly ICoreRepository<Azienda> _repository = repository;

  public async ValueTask<Result<PagedResult<AziendaDto>>> Handle(ListAziendasQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;

    if (page < 1)
    {
      return Result<PagedResult<AziendaDto>>.Error("Page must be >= 1");
    }

    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE)
    {
      return Result<PagedResult<AziendaDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");
    }

    var totalCount = await _repository.CountAsync(cancellationToken);
    var skip = (page - 1) * perPage;

    var tenants = await _repository.ListAsync(cancellationToken);
    var pagedAziendas = tenants
      .OrderBy(t => t.Name.Value)
      .Skip(skip)
      .Take(perPage)
      .ToList();

    var dtos = pagedAziendas
      .Select(t => new AziendaDto(t.Id, t.Name, t.Code, t.IsActive))
      .ToList();

    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    var result = new PagedResult<AziendaDto>(dtos, page, perPage, totalCount, totalPages);

    return Result<PagedResult<AziendaDto>>.Success(result);
  }
}
