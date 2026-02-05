using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;


namespace PrometheonSuite.Identity.Infrastructure.Data.Config.UtenteApplicazioneAziendaConfig;

public class UtenteApplicazioneAziendaFiguraConfiguration : IEntityTypeConfiguration<UtenteApplicazioneAziendaFigura>
{
  public void Configure(EntityTypeBuilder<UtenteApplicazioneAziendaFigura> builder)
  {
    builder.ToTable("UtenteApplicazioneAziendaFiguras");

    builder.HasKey(utf => new { utf.UtenteApplicazioneAziendaId, utf.FiguraId });

    builder.Property(x => x.UtenteApplicazioneAziendaId)
      .HasConversion(
        id => id.Value,
        value => UtenteApplicazioneAziendaId.From(value))
      .IsRequired();

    builder.Property(x => x.FiguraId)
      .HasConversion(
        id => id.Value,
        value => FiguraId.From(value))
      .IsRequired();
  }
}
