using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.AddFigure;

public record AddFigureToUserTenantCommand(
  UserTenantId UserTenantId,
  FigureId FigureId
) : ICommand<Result<UserTenantDto>>;
