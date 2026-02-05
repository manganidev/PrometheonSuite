using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Aziendas.Update;

public class UpdateAziendaHandler(ICoreRepository<Azienda> repository)
  : ICommandHandler<UpdateAziendaCommand, Result<AziendaDto>>
{
  private readonly ICoreRepository<Azienda> _repository = repository;

  public async ValueTask<Result<AziendaDto>> Handle(UpdateAziendaCommand request, CancellationToken cancellationToken)
  {
    var tenant = await _repository.FirstOrDefaultAsync(
      new AziendaByIdSpec(request.AziendaId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result<AziendaDto>.NotFound();
    }

    // Check if new code already exists on another tenant
    if (tenant.Code != request.Code)
    {
      var existingAzienda = await _repository.FirstOrDefaultAsync(
        new AziendaByCodeSpec(request.Code), 
        cancellationToken);

      if (existingAzienda != null && existingAzienda.Id != tenant.Id)
      {
        return Result<AziendaDto>.Error($"A tenant with code '{request.Code.Value}' already exists.");
      }
    }

    tenant.AggiornaInfo(request.Name, request.Code);
    await _repository.SaveChangesAsync(cancellationToken);

    var dto = new AziendaDto(tenant.Id, tenant.Name, tenant.Code, tenant.IsActive);
    return Result<AziendaDto>.Success(dto);
  }
}
