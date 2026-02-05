using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figuras.Delete;

public record DeleteFiguraCommand(FiguraId FiguraId) : ICommand<Result>;
