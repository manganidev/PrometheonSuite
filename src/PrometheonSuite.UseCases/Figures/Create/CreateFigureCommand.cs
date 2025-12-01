using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures.Create;

public record CreateFigureCommand(
  FigureCode Code,
  FigureName Name,
  string? Description = null,
  List<Guid>? RoleIds = null
) : ICommand<Result<FigureId>>;
