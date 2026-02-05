using System.Data;
using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
namespace  PrometheonSuite.Identity.UseCases.Ruolos.List;

public class ListRuolosHandler(ICoreRepository<Ruolo> repository)
  : IQueryHandler<ListRuolosQuery, Result<PagedResult<RuoloDto>>>
{
  private readonly ICoreRepository<Ruolo> _repository = repository;

  public async ValueTask<Result<PagedResult<RuoloDto>>> Handle(ListRuolosQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;

    if (page < 1)
    {
      return Result<PagedResult<RuoloDto>>.Error("Page must be >= 1");
    }

    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE)
    {
      return Result<PagedResult<RuoloDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");
    }

    var totalCount = await _repository.CountAsync(cancellationToken);
    var skip = (page - 1) * perPage;

    var roles = await _repository.ListAsync(cancellationToken);
    var pagedRuolos = roles
      .OrderBy(r => r.Code.Value)
      .Skip(skip)
      .Take(perPage)
      .ToList();

    var dtos = pagedRuolos
      .Select(r => new RuoloDto(r.Id, r.Code, r.Name, r.ApplicazioneId, r.Description))
      .ToList();

    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    var result = new PagedResult<RuoloDto>(dtos, page, perPage, totalCount, totalPages);

    return Result<PagedResult<RuoloDto>>.Success(result);
  }
}
