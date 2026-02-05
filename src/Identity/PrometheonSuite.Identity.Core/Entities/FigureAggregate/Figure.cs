using PrometheonSuite.Identity.BaseEntities;
using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace PrometheonSuite.Identity.Entities.FigureAggregate;

public class Figure : AuditableEntity<Figure, FigureId>, IAggregateRoot
{
  public FigureCode Code { get; private set; }
  public FigureName Name { get; private set; }
  public string? Description { get; private set; }

  private readonly List<FigureRole> _figureRoles = new();
  public IReadOnlyCollection<FigureRole> FigureRoles => _figureRoles.AsReadOnly();

  public Figure(FigureCode code, FigureName name, string? description = null)
  {
    Id = FigureId.From(Guid.NewGuid());
    Code = code;
    Name = name;
    Description = description;
  }

#pragma warning disable CS8618
  private Figure()
  {
    Id = FigureId.From(Guid.Empty);
  }
#pragma warning restore CS8618
  public static Figure CreateSeeded(
    FigureId id,
    FigureCode code,
    FigureName name,
    string? description = null)
  {
    var figure = new Figure
    {
      Id = id,
      Code = code,
      Name = name,
      Description = description
    };

    return figure;
  }
  public Figure AggiornaInfo(FigureCode nuovoCode, FigureName nuovoName, string? nuovaDescription)
  {
    Code = nuovoCode;
    Name = nuovoName;
    Description = nuovaDescription;
    return this;
  }

  public Figure AggiungiRuolo(RoleId roleId)
  {
    if (!_figureRoles.Any(fr => fr.RoleId == roleId))
    {
      _figureRoles.Add(new FigureRole(Id, roleId));
    }
    return this;
  }

  public Figure RimuoviRuolo(RoleId roleId)
  {
    var figureRole = _figureRoles.FirstOrDefault(fr => fr.RoleId == roleId);
    if (figureRole != null)
    {
      _figureRoles.Remove(figureRole);
    }
    return this;
  }
}
