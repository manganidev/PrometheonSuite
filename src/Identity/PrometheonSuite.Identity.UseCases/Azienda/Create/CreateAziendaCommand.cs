using PrometheonSuite.Identity.Entities.AziendaAggregate;

namespace  PrometheonSuite.Identity.UseCases.Aziendas.Create;

public record CreateAziendaCommand(
  AziendaName Name,
  AziendaCode Code
) : ICommand<Result<AziendaId>>;
