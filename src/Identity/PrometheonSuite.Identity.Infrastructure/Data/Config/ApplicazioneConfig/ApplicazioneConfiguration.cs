using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.ApplicazioneConfig;

public class ApplicazioneConfiguration : IEntityTypeConfiguration<Applicazione>
{
  public void Configure(EntityTypeBuilder<Applicazione> builder)
  {
    builder.ToTable("Applicazione");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => ApplicazioneId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.Name)
      .HasConversion(
        vo => vo.Value,
        v => ApplicazioneName.From(v))
      .HasMaxLength(ApplicazioneName.MaxLength)
      .IsRequired();

    builder.Property(x => x.Code)
      .HasConversion(
        vo => vo.Value,
        v => ApplicazioneCode.From(v))
      .HasMaxLength(ApplicazioneCode.MaxLength)
      .IsRequired();

    builder.HasIndex(x => x.Code).IsUnique();

    builder.Property(x => x.IsActive)
      .IsRequired();
  }
}

