using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate;


namespace PrometheonSuite.Infrastructure.Identity.Data.Config;

public class UserTenantFigureConfiguration : IEntityTypeConfiguration<UserTenantFigure>
{
  public void Configure(EntityTypeBuilder<UserTenantFigure> builder)
  {
    builder.ToTable("UserTenantFigures");

    builder.HasKey(utf => new { utf.UserTenantId, utf.FigureId });

    builder.Property(x => x.UserTenantId)
      .HasConversion(
        id => id.Value,
        value => UserTenantId.From(value))
      .IsRequired();

    builder.Property(x => x.FigureId)
      .HasConversion(
        id => id.Value,
        value => FigureId.From(value))
      .IsRequired();
  }
}
