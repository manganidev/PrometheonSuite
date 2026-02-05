using PrometheonSuite.Identity.Entities.AziendaAggregate;

namespace  PrometheonSuite.Identity.UseCases.Aziendas.Delete;

public record DeleteAziendaCommand(AziendaId AziendaId) : ICommand<Result>;
