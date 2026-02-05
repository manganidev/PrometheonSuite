using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace  PrometheonSuite.Identity.UseCases.Ruolos;

public record RuoloDto(
  RuoloId Id,
  RuoloCode Code,
  RuoloName Name,
  ApplicazioneId ApplicazioneId,
  string? Description
);
