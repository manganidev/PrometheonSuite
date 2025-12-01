using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Delete;

public class DeleteUtenteHandler(IRepository<Utente> repository)
    : ICommandHandler<DeleteUtenteCommand, Result>
{
  public async ValueTask<Result> Handle(DeleteUtenteCommand request, CancellationToken cancellationToken)
  {
    var entity = await repository.GetByIdAsync(request.UtenteId, cancellationToken);

    if (entity == null)
      return Result.NotFound();

    await repository.DeleteAsync(entity, cancellationToken);

    return Result.Success();
  }
}
