
using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace PrometheonSuite.Identity.Entities.UserTenantAggregate;

public class UserTenantFigure
{
  public UserTenantId UserTenantId { get; private set; }
  public FigureId FigureId { get; private set; }

  public UserTenantFigure(UserTenantId userTenantId, FigureId figureId)
  {
    UserTenantId = userTenantId;
    FigureId = figureId;
  }

#pragma warning disable CS8618
  private UserTenantFigure()
  {
  }
#pragma warning restore CS8618
}
