using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Get;


public class GetUtenteHandler(IReadRepository<Utente> repository)
    : IQueryHandler<GetUtenteQuery, Result<UtenteDto>>
{
  public async ValueTask<Result<UtenteDto>> Handle(GetUtenteQuery request, CancellationToken cancellationToken)
  {
    var entity = await repository.GetByIdAsync(request.UtenteId, cancellationToken);

    if (entity is null)
      return Result.NotFound();

    return new UtenteDto(entity.Id, entity.Username, entity.Email, entity.Attivo);
  }
}
