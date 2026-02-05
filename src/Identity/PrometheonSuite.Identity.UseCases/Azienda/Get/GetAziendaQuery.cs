using PrometheonSuite.Identity.Entities.AziendaAggregate;

namespace  PrometheonSuite.Identity.UseCases.Aziendas.Get;

public record GetAziendaQuery(AziendaId AziendaId) : IQuery<Result<AziendaDto>>;
