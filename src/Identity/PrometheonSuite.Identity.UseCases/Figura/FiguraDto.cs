using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figuras;

public record FiguraDto(
  FiguraId Id,
  FiguraCode Code,
  FiguraName Name,
  ApplicazioneId ApplicazioneId,
  string? Description,
  IReadOnlyList<Guid> RoleIds
);
