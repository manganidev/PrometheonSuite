using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figuras.RemoveRuolo;

public record RemoveRuoloFromFiguraCommand(
  FiguraId FiguraId,
  RuoloId RuoloId
) : ICommand<Result<FiguraDto>>;
