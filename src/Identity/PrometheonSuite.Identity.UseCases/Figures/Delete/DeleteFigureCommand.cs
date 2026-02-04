using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures.Delete;

public record DeleteFigureCommand(FigureId FigureId) : ICommand<Result>;
