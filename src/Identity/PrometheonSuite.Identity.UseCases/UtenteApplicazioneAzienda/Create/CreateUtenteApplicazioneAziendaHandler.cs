using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate.Specifications;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate.Specifications;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.Create;

public class CreateUtenteApplicazioneAziendaHandler(
  ICoreRepository<UtenteApplicazioneAzienda> userAziendaRepository,
  ICoreRepository<Utente> userRepository,
  ICoreRepository<Azienda> tenantRepository)
  : ICommandHandler<CreateUtenteApplicazioneAziendaCommand, Result<UtenteApplicazioneAziendaDto>>
{
  private readonly ICoreRepository<UtenteApplicazioneAzienda> _userAziendaRepository = userAziendaRepository;
  private readonly ICoreRepository<Utente> _userRepository = userRepository;
  private readonly ICoreRepository<Azienda> _tenantRepository = tenantRepository;

  public async ValueTask<Result<UtenteApplicazioneAziendaDto>> Handle(CreateUtenteApplicazioneAziendaCommand request, CancellationToken cancellationToken)
  {
    // Verify user exists
    var user = await _userRepository.FirstOrDefaultAsync(
      new UtenteByIdSpec(request.UserId), 
      cancellationToken);

    if (user == null)
    {
      return Result<UtenteApplicazioneAziendaDto>.NotFound("User not found");
    }

    // Verify tenant exists
    var tenant = await _tenantRepository.FirstOrDefaultAsync(
      new AziendaByIdSpec(request.AziendaId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result<UtenteApplicazioneAziendaDto>.NotFound("Azienda not found");
    }

    // Check if association already exists
    var existing = await _userAziendaRepository.FirstOrDefaultAsync(
      new UtenteApplicazioneAziendaByUserAndAziendaSpec(request.UserId, request.AziendaId), 
      cancellationToken);

    if (existing != null)
    {
      return Result<UtenteApplicazioneAziendaDto>.Error("User is already associated with this tenant");
    }

    var userAzienda = new UtenteApplicazioneAzienda(request.UserId, request.AziendaId,request.ApplicazioneId, request.DefaultLocale);
    
    var created = await _userAziendaRepository.AddAsync(userAzienda, cancellationToken);
    await _userAziendaRepository.SaveChangesAsync(cancellationToken);

    var dto = new UtenteApplicazioneAziendaDto(
      created.Id,
      created.UserId,
      created.AziendaId,
      created.ApplicazioneId,
      created.IsActive,
      created.DefaultLocale,
      new List<Guid>());

    return Result<UtenteApplicazioneAziendaDto>.Success(dto);
  }
}
