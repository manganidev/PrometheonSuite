using PrometheonSuite.Identity.Entities.AziendaAggregate;

namespace  PrometheonSuite.Identity.UseCases.Aziendas.Update;

public record UpdateAziendaCommand(
  AziendaId AziendaId,
  AziendaName Name,
  AziendaCode Code
) : ICommand<Result<AziendaDto>>;
