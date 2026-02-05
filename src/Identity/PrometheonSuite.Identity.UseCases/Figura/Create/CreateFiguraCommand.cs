using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace  PrometheonSuite.Identity.UseCases.Figuras.Create;

public record CreateFiguraCommand(
  FiguraCode Code,
  FiguraName Name,
  ApplicazioneId ApplicationId,
  string? Description = null,
  List<Guid>? RuoloIds = null
) : ICommand<Result<FiguraId>>;
