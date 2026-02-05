using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace  PrometheonSuite.Identity.UseCases.Ruolos.Update;

public record UpdateRuoloCommand(
  RuoloId RuoloId,
  RuoloCode Code,
  RuoloName Name,
  string? Description
) : ICommand<Result<RuoloDto>>;
