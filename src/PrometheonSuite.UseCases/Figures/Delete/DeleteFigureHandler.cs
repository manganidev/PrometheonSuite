using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Figures.Delete;

public class DeleteFigureHandler(IRepository<Figure> repository)
  : ICommandHandler<DeleteFigureCommand, Result>
{
  private readonly IRepository<Figure> _repository = repository;

  public async ValueTask<Result> Handle(DeleteFigureCommand request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FigureByIdSpec(request.FigureId), 
      cancellationToken);

    if (figure == null)
    {
      return Result.NotFound();
    }

    await _repository.DeleteAsync(figure, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
