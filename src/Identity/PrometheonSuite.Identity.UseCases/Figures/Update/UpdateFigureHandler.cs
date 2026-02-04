using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Figures.Update;

public class UpdateFigureHandler(ICoreRepository<Figure> repository)
  : ICommandHandler<UpdateFigureCommand, Result<FigureDto>>
{
  private readonly ICoreRepository<Figure> _repository = repository;

  public async ValueTask<Result<FigureDto>> Handle(UpdateFigureCommand request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FigureByIdSpec(request.FigureId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FigureDto>.NotFound();
    }

    // Check if new code already exists on another figure
    if (figure.Code != request.Code)
    {
      var existingFigure = await _repository.FirstOrDefaultAsync(
        new FigureByCodeSpec(request.Code), 
        cancellationToken);

      if (existingFigure != null && existingFigure.Id != figure.Id)
      {
        return Result<FigureDto>.Error($"A figure with code '{request.Code.Value}' already exists.");
      }
    }

    figure.AggiornaInfo(request.Code, request.Name, request.Description);
    await _repository.SaveChangesAsync(cancellationToken);

    var roleIds = figure.FigureRoles.Select(fr => fr.RoleId.Value).ToList();
    var dto = new FigureDto(figure.Id, figure.Code, figure.Name, figure.Description, roleIds);
    
    return Result<FigureDto>.Success(dto);
  }
}
