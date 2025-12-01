using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.TenantAggregate;


namespace PrometheonSuite.Infrastructure.Identity.Data.Config;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
  public void Configure(EntityTypeBuilder<Tenant> builder)
  {
    builder.ToTable("Tenants");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => TenantId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.Name)
      .HasConversion(
        vo => vo.Value,
        v => TenantName.From(v))
      .HasMaxLength(TenantName.MaxLength)
      .IsRequired();

    builder.Property(x => x.Code)
      .HasConversion(
        vo => vo.Value,
        v => TenantCode.From(v))
      .HasMaxLength(TenantCode.MaxLength)
      .IsRequired();

    builder.HasIndex(x => x.Code).IsUnique();

    builder.Property(x => x.IsActive)
      .IsRequired();
  }
}
