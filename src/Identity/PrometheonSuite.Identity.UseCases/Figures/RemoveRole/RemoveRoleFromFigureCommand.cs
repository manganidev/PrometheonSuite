using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures.RemoveRole;

public record RemoveRoleFromFigureCommand(
  FigureId FigureId,
  RoleId RoleId
) : ICommand<Result<FigureDto>>;
