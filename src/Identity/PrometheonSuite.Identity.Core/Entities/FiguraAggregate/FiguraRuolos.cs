

using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace PrometheonSuite.Identity.Entities.FiguraAggregate;

public class FiguraRuolo
{
  public FiguraId FiguraId { get; private set; }
  public RuoloId RuoloId { get; private set; }

  public FiguraRuolo(FiguraId figureId, RuoloId roleId)
  {
    FiguraId = figureId;
    RuoloId = roleId;
  }

#pragma warning disable CS8618
  private FiguraRuolo()
  {
  }
#pragma warning restore CS8618
}
