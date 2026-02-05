using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Ruolos.Update;

public class UpdateRuoloHandler(ICoreRepository<Ruolo> repository)
  : ICommandHandler<UpdateRuoloCommand, Result<RuoloDto>>
{
  private readonly ICoreRepository<Ruolo> _repository = repository;

  public async ValueTask<Result<RuoloDto>> Handle(UpdateRuoloCommand request, CancellationToken cancellationToken)
  {
    var role = await _repository.FirstOrDefaultAsync(
      new RuoloByIdSpec(request.RuoloId), 
      cancellationToken);

    if (role == null)
    {
      return Result<RuoloDto>.NotFound();
    }

    // Check if new code already exists on another role
    if (role.Code != request.Code)
    {
      var existingRuolo = await _repository.FirstOrDefaultAsync(
        new RuoloByCodeSpec(request.Code), 
        cancellationToken);

      if (existingRuolo != null && existingRuolo.Id != role.Id)
      {
        return Result<RuoloDto>.Error($"A role with code '{request.Code.Value}' already exists.");
      }
    }

    role.AggiornaInfo(request.Code, request.Name, request.Description);
    await _repository.SaveChangesAsync(cancellationToken);

    var dto = new RuoloDto(role.Id, role.Code, role.Name,role.ApplicazioneId, role.Description);
    return Result<RuoloDto>.Success(dto);
  }
}
