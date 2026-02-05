using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;
using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figuras.Create;

public class CreateFiguraHandler(ICoreRepository<Figura> figureRepository, ICoreRepository<Ruolo> roleRepository)
  : ICommandHandler<CreateFiguraCommand, Result<FiguraId>>
{
  private readonly ICoreRepository<Figura> _figureRepository = figureRepository;
  private readonly ICoreRepository<Ruolo> _roleRepository = roleRepository;

  public async ValueTask<Result<FiguraId>> Handle(CreateFiguraCommand request, CancellationToken cancellationToken)
  {
    // Check if figure code already exists
    var existingFigura = await _figureRepository.FirstOrDefaultAsync(
      new FiguraByCodeSpec(request.Code), 
      cancellationToken);

    if (existingFigura != null)
    {
      return Result<FiguraId>.Error($"A figure with code '{request.Code.Value}' already exists.");
    }

    var figure = new Figura(request.Code, request.Name,request.ApplicationId, request.Description);

    // Add roles if provided
    if (request.RuoloIds != null && request.RuoloIds.Count > 0)
    {
      foreach (var roleIdValue in request.RuoloIds)
      {
        var roleId = RuoloId.From(roleIdValue);
        figure.AggiungiRuolo(roleId);
      }
    }
    
    var createdFigura = await _figureRepository.AddAsync(figure, cancellationToken);
    await _figureRepository.SaveChangesAsync(cancellationToken);

    return Result<FiguraId>.Success(createdFigura.Id);
  }
}
