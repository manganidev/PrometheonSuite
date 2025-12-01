

using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace PrometheonSuite.Identity.Entities.FigureAggregate;

public class FigureRole
{
  public FigureId FigureId { get; private set; }
  public RoleId RoleId { get; private set; }

  public FigureRole(FigureId figureId, RoleId roleId)
  {
    FigureId = figureId;
    RoleId = roleId;
  }

#pragma warning disable CS8618
  private FigureRole()
  {
  }
#pragma warning restore CS8618
}
