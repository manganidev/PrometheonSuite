using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.UseCases.Figuras;

namespace  PrometheonSuite.Identity.UseCases.Figuras.AddRuolo;

public record AddRuoloToFiguraCommand(
  FiguraId FiguraId,
  RuoloId RuoloId
) : ICommand<Result<FiguraDto>>;
