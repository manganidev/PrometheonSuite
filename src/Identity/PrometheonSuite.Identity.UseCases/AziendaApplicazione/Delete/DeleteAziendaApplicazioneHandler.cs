using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate.Specifications;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Delete;

public class DeleteAziendaApplicazioneHandler(ICoreRepository<AziendaApplicazione> repository) : ICommandHandler<DeleteAziendaApplicazioneCommand, Result>
{
  public async ValueTask<Result> Handle(DeleteAziendaApplicazioneCommand request, CancellationToken cancellationToken)
  {
    var entity = await repository.FirstOrDefaultAsync(new AziendaApplicazioneByIdSpec(request.AziendaApplicazioneId), cancellationToken);
    if (entity == null) return Result.NotFound();

    await repository.DeleteAsync(entity, cancellationToken);
    await repository.SaveChangesAsync(cancellationToken);
    return Result.Success();
  }
}
