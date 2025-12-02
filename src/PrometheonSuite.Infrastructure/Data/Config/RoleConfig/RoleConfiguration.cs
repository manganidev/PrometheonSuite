using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.RoleAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.RoleConfig;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.ToTable("Roles");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => RoleId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.Code)
      .HasConversion(
        vo => vo.Value,
        v => RoleCode.From(v))
      .HasMaxLength(RoleCode.MaxLength)
      .IsRequired();

    builder.Property(x => x.Name)
      .HasConversion(
        vo => vo.Value,
        v => RoleName.From(v))
      .HasMaxLength(RoleName.MaxLength)
      .IsRequired();

    builder.HasIndex(x => x.Code).IsUnique();

    builder.Property(x => x.Description)
      .HasMaxLength(500);
  }
}
