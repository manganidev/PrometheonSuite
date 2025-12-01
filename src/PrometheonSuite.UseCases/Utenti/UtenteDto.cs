

using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti;

public record UtenteDto(
    UtenteId Id,
    Username Username,
    Email Email,
    bool Attivo
);
