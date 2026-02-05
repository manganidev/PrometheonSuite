using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace  PrometheonSuite.Identity.UseCases.Ruolos.Delete;

public record DeleteRuoloCommand(RuoloId RuoloId) : ICommand<Result>;
