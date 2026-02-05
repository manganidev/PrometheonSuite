using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace  PrometheonSuite.Identity.UseCases.Ruolos.Get;

public record GetRuoloQuery(RuoloId RuoloId) : IQuery<Result<RuoloDto>>;
