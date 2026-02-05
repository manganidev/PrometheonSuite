using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Figuras.Delete;

public class DeleteFiguraHandler(ICoreRepository<Figura> repository)
  : ICommandHandler<DeleteFiguraCommand, Result>
{
  private readonly ICoreRepository<Figura> _repository = repository;

  public async ValueTask<Result> Handle(DeleteFiguraCommand request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FiguraByIdSpec(request.FiguraId), 
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
