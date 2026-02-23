using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate.Specifications;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Get;

public class GetAziendaApplicazioneHandler(ICoreRepository<AziendaApplicazione> repository) : IQueryHandler<GetAziendaApplicazioneQuery, Result<AziendaApplicazioneDto>>
{
  public async ValueTask<Result<AziendaApplicazioneDto>> Handle(GetAziendaApplicazioneQuery request, CancellationToken cancellationToken)
  {
    var entity = await repository.FirstOrDefaultAsync(new AziendaApplicazioneByIdSpec(request.AziendaApplicazioneId), cancellationToken);
    return entity == null
      ? Result<AziendaApplicazioneDto>.NotFound()
      : Result<AziendaApplicazioneDto>.Success(new AziendaApplicazioneDto(entity.Id, entity.AziendaId, entity.ApplicazioneId, entity.IsActive));
  }
}
