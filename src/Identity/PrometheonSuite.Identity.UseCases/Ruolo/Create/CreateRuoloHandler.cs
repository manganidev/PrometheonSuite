using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Ruolos.Create;

public class CreateRuoloHandler(ICoreRepository<Ruolo> repository)
  : ICommandHandler<CreateRuoloCommand, Result<RuoloId>>
{
  private readonly ICoreRepository<Ruolo> _repository = repository;

  public async ValueTask<Result<RuoloId>> Handle(CreateRuoloCommand request, CancellationToken cancellationToken)
  {
    // Check if role code already exists
    var existingRuolo = await _repository.FirstOrDefaultAsync(
      new RuoloByCodeSpec(request.Code), 
      cancellationToken);

    if (existingRuolo != null)
    {
      return Result<RuoloId>.Error($"A role with code '{request.Code.Value}' already exists.");
    }

    var role = new Ruolo(request.Code, request.Name,request.ApplicazioneId, request.Description);
    
    var createdRuolo = await _repository.AddAsync(role, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<RuoloId>.Success(createdRuolo.Id);
  }
}
