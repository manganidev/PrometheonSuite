using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.UtenteApplicazioneAziendaConfig;

public class UtenteApplicazioneAziendaConfiguration : IEntityTypeConfiguration<UtenteApplicazioneAzienda>
{
  public void Configure(EntityTypeBuilder<UtenteApplicazioneAzienda> builder)
  {
    builder.ToTable("UtenteApplicazioneAzienda");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => UtenteApplicazioneAziendaId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.UserId)
      .HasConversion(
        id => id.Value,
        value => UtenteId.From(value))
      .IsRequired();

    builder.Property(x => x.AziendaId)
      .HasConversion(
        id => id.Value,
        value => AziendaId.From(value))
      .IsRequired();
    builder.Property(x => x.ApplicazioneId)
     .HasConversion(
       id => id.Value,
       value => ApplicazioneId.From(value))
     .IsRequired();
    builder.HasIndex(ut => new { ut.UserId, ut.AziendaId,ut.ApplicazioneId }).IsUnique();

    builder.Property(x => x.IsActive)
      .IsRequired();

    builder.Property(x => x.DefaultLocale)
      .HasMaxLength(10);

    builder.HasMany(ut => ut.UtenteApplicazioneAziendaFiguras)
      .WithOne()
      .HasForeignKey(utf => utf.UtenteApplicazioneAziendaId)
      .OnDelete(DeleteBehavior.Cascade);


  }
}
