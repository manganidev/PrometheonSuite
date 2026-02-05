using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;


namespace PrometheonSuite.Identity.Infrastructure.Data.Config.FiguraConfig;

public class FiguraRuoloConfiguration : IEntityTypeConfiguration<FiguraRuolo>
{
  public void Configure(EntityTypeBuilder<FiguraRuolo> builder)
  {
    builder.ToTable("FiguraRuolos");

    builder.HasKey(fr => new { fr.FiguraId, fr.RuoloId });

    builder.Property(x => x.FiguraId)
      .HasConversion(
        id => id.Value,
        value => FiguraId.From(value))
      .IsRequired();

    builder.Property(x => x.RuoloId)
      .HasConversion(
        id => id.Value,
        value => RuoloId.From(value))
      .IsRequired();
  }
}
