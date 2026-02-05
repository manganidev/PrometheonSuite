using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figuras.Get;

public record GetFiguraQuery(FiguraId FiguraId) : IQuery<Result<FiguraDto>>;
