using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Ruolos.Get;

public class GetRuoloHandler(ICoreRepository<Ruolo> repository)
  : IQueryHandler<GetRuoloQuery, Result<RuoloDto>>
{
  private readonly ICoreRepository<Ruolo> _repository = repository;

  public async ValueTask<Result<RuoloDto>> Handle(GetRuoloQuery request, CancellationToken cancellationToken)
  {
    var role = await _repository.FirstOrDefaultAsync(
      new RuoloByIdSpec(request.RuoloId), 
      cancellationToken);

    if (role == null)
    {
      return Result<RuoloDto>.NotFound();
    }

    var dto = new RuoloDto(role.Id, role.Code, role.Name,role.ApplicazioneId, role.Description);
    return Result<RuoloDto>.Success(dto);
  }
}
