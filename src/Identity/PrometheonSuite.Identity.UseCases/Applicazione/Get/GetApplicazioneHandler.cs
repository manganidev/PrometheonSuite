using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate.Specifications;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Get;

public class GetApplicazioneHandler(ICoreRepository<Applicazione> repository) : IQueryHandler<GetApplicazioneQuery, Result<ApplicazioneDto>>
{
  public async ValueTask<Result<ApplicazioneDto>> Handle(GetApplicazioneQuery request, CancellationToken cancellationToken)
  {
    var app = await repository.FirstOrDefaultAsync(new ApplicazioneByIdSpec(request.ApplicazioneId), cancellationToken);
    return app == null
      ? Result<ApplicazioneDto>.NotFound()
      : Result<ApplicazioneDto>.Success(new ApplicazioneDto(app.Id, app.Name, app.Code, app.IsActive));
  }
}
