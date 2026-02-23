using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate.Specifications;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Update;

public class UpdateApplicazioneHandler(ICoreRepository<Applicazione> repository) : ICommandHandler<UpdateApplicazioneCommand, Result<ApplicazioneDto>>
{
  public async ValueTask<Result<ApplicazioneDto>> Handle(UpdateApplicazioneCommand request, CancellationToken cancellationToken)
  {
    var app = await repository.FirstOrDefaultAsync(new ApplicazioneByIdSpec(request.ApplicazioneId), cancellationToken);
    if (app == null) return Result<ApplicazioneDto>.NotFound();

    if (app.Code != request.Code)
    {
      var existing = await repository.FirstOrDefaultAsync(new ApplicazioneByCodeSpec(request.Code), cancellationToken);
      if (existing != null && existing.Id != app.Id) return Result<ApplicazioneDto>.Error($"An application with code '{request.Code.Value}' already exists.");
    }

    app.AggiornaInfo(request.Name, request.Code);
    await repository.SaveChangesAsync(cancellationToken);
    return Result<ApplicazioneDto>.Success(new ApplicazioneDto(app.Id, app.Name, app.Code, app.IsActive));
  }
}
