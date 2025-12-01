using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures.Update;

public record UpdateFigureCommand(
  FigureId FigureId,
  FigureCode Code,
  FigureName Name,
  string? Description
) : ICommand<Result<FigureDto>>;
