using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate.Specifications;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Create;

public class CreateApplicazioneHandler(ICoreRepository<Applicazione> repository) : ICommandHandler<CreateApplicazioneCommand, Result<ApplicazioneId>>
{
  public async ValueTask<Result<ApplicazioneId>> Handle(CreateApplicazioneCommand request, CancellationToken cancellationToken)
  {
    var existing = await repository.FirstOrDefaultAsync(new ApplicazioneByCodeSpec(request.Code), cancellationToken);
    if (existing != null) return Result<ApplicazioneId>.Error($"An application with code '{request.Code.Value}' already exists.");

    var created = await repository.AddAsync(new Applicazione(request.Name, request.Code), cancellationToken);
    await repository.SaveChangesAsync(cancellationToken);
    return Result<ApplicazioneId>.Success(created.Id);
  }
}
