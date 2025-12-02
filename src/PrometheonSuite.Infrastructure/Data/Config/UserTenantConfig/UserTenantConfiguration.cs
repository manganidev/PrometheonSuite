using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.UserTenantConfig;

public class UserTenantConfiguration : IEntityTypeConfiguration<UserTenant>
{
  public void Configure(EntityTypeBuilder<UserTenant> builder)
  {
    builder.ToTable("UserTenants");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => UserTenantId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.UserId)
      .HasConversion(
        id => id.Value,
        value => UtenteId.From(value))
      .IsRequired();

    builder.Property(x => x.TenantId)
      .HasConversion(
        id => id.Value,
        value => TenantId.From(value))
      .IsRequired();

    builder.HasIndex(ut => new { ut.UserId, ut.TenantId }).IsUnique();

    builder.Property(x => x.IsActive)
      .IsRequired();

    builder.Property(x => x.DefaultLocale)
      .HasMaxLength(10);

    builder.HasMany(ut => ut.UserTenantFigures)
      .WithOne()
      .HasForeignKey(utf => utf.UserTenantId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(ut => ut.UserTenantRoles)
      .WithOne()
      .HasForeignKey(utr => utr.UserTenantId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
