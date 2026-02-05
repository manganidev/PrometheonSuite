using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.RuoloConfig;

public class RuoloConfiguration : IEntityTypeConfiguration<Ruolo>
{
  public void Configure(EntityTypeBuilder<Ruolo> builder)
  {
    builder.ToTable("Ruolo");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => RuoloId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.ApplicazioneId)
  .HasConversion(
    id => id.Value,
    value => ApplicazioneId.From(value))
  .IsRequired();

    builder.HasOne<Applicazione>()
  .WithMany()
  .HasForeignKey(x => x.ApplicazioneId)
  .OnDelete(DeleteBehavior.Restrict);


    builder.Property(x => x.Code)
      .HasConversion(
        vo => vo.Value,
        v => RuoloCode.From(v))
      .HasMaxLength(RuoloCode.MaxLength)
      .IsRequired();

    builder.Property(x => x.Name)
      .HasConversion(
        vo => vo.Value,
        v => RuoloName.From(v))
      .HasMaxLength(RuoloName.MaxLength)
      .IsRequired();

    builder.HasIndex(x => new { x.ApplicazioneId, x.Code }).IsUnique();

    builder.Property(x => x.Description)
      .HasMaxLength(500);
  }
}
