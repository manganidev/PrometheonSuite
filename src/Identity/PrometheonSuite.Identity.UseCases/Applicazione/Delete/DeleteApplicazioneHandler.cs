using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate.Specifications;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Delete;

public class DeleteApplicazioneHandler(ICoreRepository<Applicazione> repository) : ICommandHandler<DeleteApplicazioneCommand, Result>
{
  public async ValueTask<Result> Handle(DeleteApplicazioneCommand request, CancellationToken cancellationToken)
  {
    var app = await repository.FirstOrDefaultAsync(new ApplicazioneByIdSpec(request.ApplicazioneId), cancellationToken);
    if (app == null) return Result.NotFound();

    await repository.DeleteAsync(app, cancellationToken);
    await repository.SaveChangesAsync(cancellationToken);
    return Result.Success();
  }
}
