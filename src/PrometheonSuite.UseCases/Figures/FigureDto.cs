using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figures;

public record FigureDto(
  FigureId Id,
  FigureCode Code,
  FigureName Name,
  string? Description,
  IReadOnlyList<Guid> RoleIds
);
