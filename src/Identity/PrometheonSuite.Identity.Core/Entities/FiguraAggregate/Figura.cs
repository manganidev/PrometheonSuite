using PrometheonSuite.Identity.BaseEntities;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace PrometheonSuite.Identity.Entities.FiguraAggregate;

public class Figura : AuditableEntity<Figura, FiguraId>, IAggregateRoot
{
  public FiguraCode Code { get; private set; }
  public FiguraName Name { get; private set; }
  public string? Description { get; private set; }
  public ApplicazioneId ApplicazioneId { get; private set; }
  private readonly List<FiguraRuolo> _figureRuolos = new();
  public IReadOnlyCollection<FiguraRuolo> FiguraRuolos => _figureRuolos.AsReadOnly();

  public Figura(FiguraCode code, FiguraName name, ApplicazioneId applicationId, string? description = null)
  {
    Id = FiguraId.From(Guid.NewGuid());
    Code = code;
    Name = name;
    ApplicazioneId = applicationId;
    Description = description;
  }

#pragma warning disable CS8618
  private Figura()
  {
    Id = FiguraId.From(Guid.Empty);
  }
#pragma warning restore CS8618
  public static Figura CreateSeeded(
    FiguraId id,
    FiguraCode code,
    FiguraName name,
    string? description = null)
  {
    var figure = new Figura
    {
      Id = id,
      Code = code,
      Name = name,
      Description = description
    };

    return figure;
  }
  public Figura AggiornaInfo(FiguraCode nuovoCode, FiguraName nuovoName, string? nuovaDescription)
  {
    Code = nuovoCode;
    Name = nuovoName;
    Description = nuovaDescription;
    return this;
  }

  public Figura AggiungiRuolo(RuoloId roleId)
  {
    if (!_figureRuolos.Any(fr => fr.RuoloId == roleId))
    {
      _figureRuolos.Add(new FiguraRuolo(Id, roleId));
    }
    return this;
  }

  public Figura RimuoviRuolo(RuoloId roleId)
  {
    var figureRuolo = _figureRuolos.FirstOrDefault(fr => fr.RuoloId == roleId);
    if (figureRuolo != null)
    {
      _figureRuolos.Remove(figureRuolo);
    }
    return this;
  }
}
