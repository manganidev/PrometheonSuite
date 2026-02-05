using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.AziendaApplicazioneConfig;

public class AziendaApplicazioneConfiguration : IEntityTypeConfiguration<AziendaApplicazione>
{
  public void Configure(EntityTypeBuilder<AziendaApplicazione> builder)
  {
    builder.ToTable("AziendaApplicazione");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(id => id.Value, value => AziendaApplicazioneId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.AziendaId)
      .HasConversion(id => id.Value, value => AziendaId.From(value))
      .IsRequired();

    builder.Property(x => x.ApplicazioneId)
      .HasConversion(id => id.Value, value => ApplicazioneId.From(value))
      .IsRequired();

    builder.HasIndex(x => new { x.AziendaId, x.ApplicazioneId }).IsUnique();

    builder.Property(x => x.IsActive).IsRequired();

    builder.HasOne<Azienda>()
      .WithMany()
      .HasForeignKey(x => x.AziendaId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne<Applicazione>()
      .WithMany()
      .HasForeignKey(x => x.ApplicazioneId)
      .OnDelete(DeleteBehavior.Restrict);
  }

}
