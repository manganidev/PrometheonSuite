using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Comand.Update;

public class UpdateUtenteHandler(ICoreRepository<Utente> repository)
    : ICommandHandler<UpdateUtenteCommand, Result<UtenteDto>>
{
  public async ValueTask<Result<UtenteDto>> Handle(UpdateUtenteCommand request, CancellationToken cancellationToken)
  {
    var entity = await repository.GetByIdAsync(request.UtenteId, cancellationToken);

    if (entity is null)
      return Result.NotFound();

    entity.AggiornaAnagrafica(request.Username, request.Email);

    await repository.UpdateAsync(entity, cancellationToken);

    return new UtenteDto(entity.Id, entity.Username, entity.Email, entity.Attivo);
  }
}
