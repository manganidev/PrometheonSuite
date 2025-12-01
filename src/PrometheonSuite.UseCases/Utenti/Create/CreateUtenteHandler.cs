using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Create;

public class CreateUtenteHandler(IRepository<Utente> repository)
  : ICommandHandler<CreateUtenteCommand, Result<UtenteDto>>
{
  public async ValueTask<Result<UtenteDto>> Handle(CreateUtenteCommand request, CancellationToken cancellationToken)
  {
    // Hash della password prima di creare l'entità
    var hashedPassword = HashedPassword.FromPlainText(request.Password);
    
    var entity = new Utente(request.Username, request.Email, hashedPassword);

    await repository.AddAsync(entity, cancellationToken);

    return new UtenteDto(entity.Id, entity.Username, entity.Email, entity.Attivo);
  }
}
