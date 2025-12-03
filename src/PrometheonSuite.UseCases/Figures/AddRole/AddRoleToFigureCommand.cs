using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.UseCases.Figures;

namespace  PrometheonSuite.Identity.UseCases.Figures.AddRole;

public record AddRoleToFigureCommand(
  FigureId FigureId,
  RoleId RoleId
) : ICommand<Result<FigureDto>>;
