using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Aziendas.Get;

public class GetAziendaHandler(ICoreRepository<Azienda> repository)
  : IQueryHandler<GetAziendaQuery, Result<AziendaDto>>
{
  private readonly ICoreRepository<Azienda> _repository = repository;

  public async ValueTask<Result<AziendaDto>> Handle(GetAziendaQuery request, CancellationToken cancellationToken)
  {
    var tenant = await _repository.FirstOrDefaultAsync(
      new AziendaByIdSpec(request.AziendaId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result<AziendaDto>.NotFound();
    }

    var dto = new AziendaDto(tenant.Id, tenant.Name, tenant.Code, tenant.IsActive);
    return Result<AziendaDto>.Success(dto);
  }
}
