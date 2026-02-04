using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures.Get;

public record GetFigureQuery(FigureId FigureId) : IQuery<Result<FigureDto>>;
