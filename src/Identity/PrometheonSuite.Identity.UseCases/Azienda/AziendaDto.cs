using PrometheonSuite.Identity.Entities.AziendaAggregate;

namespace  PrometheonSuite.Identity.UseCases.Aziendas;

public record AziendaDto(
  AziendaId Id,
  AziendaName Name,
  AziendaCode Code,
  bool IsActive
);
