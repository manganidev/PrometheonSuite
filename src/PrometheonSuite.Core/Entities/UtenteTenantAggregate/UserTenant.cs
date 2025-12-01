

using PrometheonSuite.Identity.BaseEntities;
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Entities.UserTenantAggregate;

public class UserTenant : AuditableEntity<UserTenant, UserTenantId>, IAggregateRoot
{
  public UtenteId UserId { get; private set; }
  public TenantId TenantId { get; private set; }
  public bool IsActive { get; private set; } = true;
  public string? DefaultLocale { get; private set; }

  private readonly List<UserTenantFigure> _userTenantFigures = new();
  public IReadOnlyCollection<UserTenantFigure> UserTenantFigures => _userTenantFigures.AsReadOnly();

  private readonly List<UserTenantRole> _userTenantRoles = new();
  public IReadOnlyCollection<UserTenantRole> UserTenantRoles => _userTenantRoles.AsReadOnly();

  public UserTenant(UtenteId userId, TenantId tenantId, string? defaultLocale = null)
  {
    Id = UserTenantId.From(Guid.NewGuid());
    UserId = userId;
    TenantId = tenantId;
    DefaultLocale = defaultLocale;
  }

#pragma warning disable CS8618
  private UserTenant()
  {
    Id = UserTenantId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public UserTenant Disattiva()
  {
    IsActive = false;
    return this;
  }

  public UserTenant Attiva()
  {
    IsActive = true;
    return this;
  }

  public UserTenant AggiornaLocale(string? nuovaLocale)
  {
    DefaultLocale = nuovaLocale;
    return this;
  }

  public UserTenant AggiungiFigura(FigureId figureId)
  {
    if (!_userTenantFigures.Any(utf => utf.FigureId == figureId))
    {
      _userTenantFigures.Add(new UserTenantFigure(Id, figureId));
    }
    return this;
  }

  public UserTenant RimuoviFigura(FigureId figureId)
  {
    var userTenantFigure = _userTenantFigures.FirstOrDefault(utf => utf.FigureId == figureId);
    if (userTenantFigure != null)
    {
      _userTenantFigures.Remove(userTenantFigure);
    }
    return this;
  }

  public UserTenant AggiungiRuoloDiretto(RoleId roleId)
  {
    if (!_userTenantRoles.Any(utr => utr.RoleId == roleId))
    {
      _userTenantRoles.Add(new UserTenantRole(Id, roleId));
    }
    return this;
  }

  public UserTenant RimuoviRuoloDiretto(RoleId roleId)
  {
    var userTenantRole = _userTenantRoles.FirstOrDefault(utr => utr.RoleId == roleId);
    if (userTenantRole != null)
    {
      _userTenantRoles.Remove(userTenantRole);
    }
    return this;
  }
}
