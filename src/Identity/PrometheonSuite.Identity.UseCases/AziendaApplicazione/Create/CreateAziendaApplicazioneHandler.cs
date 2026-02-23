using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate.Specifications;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Create;

public class CreateAziendaApplicazioneHandler(ICoreRepository<AziendaApplicazione> repository, ICoreRepository<Azienda> aziendaRepository, ICoreRepository<Applicazione> applicazioneRepository)
  : ICommandHandler<CreateAziendaApplicazioneCommand, Result<AziendaApplicazioneDto>>
{
  public async ValueTask<Result<AziendaApplicazioneDto>> Handle(CreateAziendaApplicazioneCommand request, CancellationToken cancellationToken)
  {
    if (await aziendaRepository.FirstOrDefaultAsync(new AziendaByIdSpec(request.AziendaId), cancellationToken) == null)
      return Result<AziendaApplicazioneDto>.NotFound("Azienda not found");

    if (await applicazioneRepository.FirstOrDefaultAsync(new ApplicazioneByIdSpec(request.ApplicazioneId), cancellationToken) == null)
      return Result<AziendaApplicazioneDto>.NotFound("Applicazione not found");

    var existing = (await repository.ListAsync(cancellationToken)).FirstOrDefault(x => x.AziendaId == request.AziendaId && x.ApplicazioneId == request.ApplicazioneId);
    if (existing != null) return Result<AziendaApplicazioneDto>.Error("Association already exists");

    var entity = await repository.AddAsync(new AziendaApplicazione(request.AziendaId, request.ApplicazioneId), cancellationToken);
    await repository.SaveChangesAsync(cancellationToken);
    return Result<AziendaApplicazioneDto>.Success(new AziendaApplicazioneDto(entity.Id, entity.AziendaId, entity.ApplicazioneId, entity.IsActive));
  }
}
