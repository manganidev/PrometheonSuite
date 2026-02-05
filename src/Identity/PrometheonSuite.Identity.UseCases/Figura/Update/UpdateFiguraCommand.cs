using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figuras.Update;

public record UpdateFiguraCommand(
  FiguraId FiguraId,
  FiguraCode Code,
  FiguraName Name,
  string? Description
) : ICommand<Result<FiguraDto>>;
