using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate;


namespace PrometheonSuite.Infrastructure.Identity.Data.Config;

public class FigureRoleConfiguration : IEntityTypeConfiguration<FigureRole>
{
  public void Configure(EntityTypeBuilder<FigureRole> builder)
  {
    builder.ToTable("FigureRoles");

    builder.HasKey(fr => new { fr.FigureId, fr.RoleId });

    builder.Property(x => x.FigureId)
      .HasConversion(
        id => id.Value,
        value => FigureId.From(value))
      .IsRequired();

    builder.Property(x => x.RoleId)
      .HasConversion(
        id => id.Value,
        value => RoleId.From(value))
      .IsRequired();
  }
}
