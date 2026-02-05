using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace  PrometheonSuite.Identity.UseCases.Ruolos.Create;

public record CreateRuoloCommand(
  RuoloCode Code,
  RuoloName Name,
  ApplicazioneId ApplicazioneId,
  string? Description = null
) : ICommand<Result<RuoloId>>;
