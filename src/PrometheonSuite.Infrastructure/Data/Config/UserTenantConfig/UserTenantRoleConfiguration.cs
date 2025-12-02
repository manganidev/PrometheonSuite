using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.UserTenantConfig;

public class UserTenantRoleConfiguration : IEntityTypeConfiguration<UserTenantRole>
{
  public void Configure(EntityTypeBuilder<UserTenantRole> builder)
  {
    builder.ToTable("UserTenantRoles");

    builder.HasKey(utr => new { utr.UserTenantId, utr.RoleId });

    builder.Property(x => x.UserTenantId)
      .HasConversion(
        id => id.Value,
        value => UserTenantId.From(value))
      .IsRequired();

    builder.Property(x => x.RoleId)
      .HasConversion(
        id => id.Value,
        value => RoleId.From(value))
      .IsRequired();
  }
}
