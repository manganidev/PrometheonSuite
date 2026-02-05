using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Aziendas.Delete;

public class DeleteAziendaHandler(ICoreRepository<Azienda> repository)
  : ICommandHandler<DeleteAziendaCommand, Result>
{
  private readonly ICoreRepository<Azienda> _repository = repository;

  public async ValueTask<Result> Handle(DeleteAziendaCommand request, CancellationToken cancellationToken)
  {
    var tenant = await _repository.FirstOrDefaultAsync(
      new AziendaByIdSpec(request.AziendaId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result.NotFound();
    }

    // Soft delete by setting IsActive to false
    tenant.Disattiva();
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
