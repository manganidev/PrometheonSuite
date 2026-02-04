using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.RemoveFigure;

public record RemoveFigureFromUserTenantCommand(
  UserTenantId UserTenantId,
  FigureId FigureId
) : ICommand<Result<UserTenantDto>>;
