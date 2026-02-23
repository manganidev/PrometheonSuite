using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas;

namespace PrometheonSuite.Identity.Web.Endpoints.UtenteApplicazioniAzienda;

public sealed record UtenteApplicazioneAziendaResponse(
  Guid Id,
  Guid UserId,
  Guid AziendaId,
  Guid ApplicazioneId,
  bool IsActive,
  string? DefaultLocale,
  IReadOnlyList<Guid> FiguraIds);

public sealed class UtenteApplicazioneAziendaIdRequest
{
  public Guid UtenteApplicazioneAziendaId { get; init; }
}

public sealed class UtenteApplicazioneAziendaFiguraRequest
{
  public Guid UtenteApplicazioneAziendaId { get; init; }
  public Guid FiguraId { get; init; }
}

public static class UtenteApplicazioneAziendaMapper
{
  public static UtenteApplicazioneAziendaResponse MapResponse(UtenteApplicazioneAziendaDto dto)
    => new(dto.Id.Value, dto.UserId.Value, dto.TenantId.Value, dto.ApplicazioneId.Value, dto.IsActive, dto.DefaultLocale, dto.FigureIds);
}
