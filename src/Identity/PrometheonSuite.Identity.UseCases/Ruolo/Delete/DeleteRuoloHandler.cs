using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Ruolos.Delete;

public class DeleteRuoloHandler(ICoreRepository<Ruolo> repository)
  : ICommandHandler<DeleteRuoloCommand, Result>
{
  private readonly ICoreRepository<Ruolo> _repository = repository;

  public async ValueTask<Result> Handle(DeleteRuoloCommand request, CancellationToken cancellationToken)
  {
    var role = await _repository.FirstOrDefaultAsync(
      new RuoloByIdSpec(request.RuoloId), 
      cancellationToken);

    if (role == null)
    {
      return Result.NotFound();
    }

    // Hard delete for roles (or implement soft delete if needed)
    await _repository.DeleteAsync(role, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
