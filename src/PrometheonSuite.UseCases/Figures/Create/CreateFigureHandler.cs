using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;
using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures.Create;

public class CreateFigureHandler(ICoreRepository<Figure> figureRepository, ICoreRepository<Role> roleRepository)
  : ICommandHandler<CreateFigureCommand, Result<FigureId>>
{
  private readonly ICoreRepository<Figure> _figureRepository = figureRepository;
  private readonly ICoreRepository<Role> _roleRepository = roleRepository;

  public async ValueTask<Result<FigureId>> Handle(CreateFigureCommand request, CancellationToken cancellationToken)
  {
    // Check if figure code already exists
    var existingFigure = await _figureRepository.FirstOrDefaultAsync(
      new FigureByCodeSpec(request.Code), 
      cancellationToken);

    if (existingFigure != null)
    {
      return Result<FigureId>.Error($"A figure with code '{request.Code.Value}' already exists.");
    }

    var figure = new Figure(request.Code, request.Name, request.Description);

    // Add roles if provided
    if (request.RoleIds != null && request.RoleIds.Count > 0)
    {
      foreach (var roleIdValue in request.RoleIds)
      {
        var roleId = RoleId.From(roleIdValue);
        figure.AggiungiRuolo(roleId);
      }
    }
    
    var createdFigure = await _figureRepository.AddAsync(figure, cancellationToken);
    await _figureRepository.SaveChangesAsync(cancellationToken);

    return Result<FigureId>.Success(createdFigure.Id);
  }
}
