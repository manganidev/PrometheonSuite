using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data.Config.FiguraConfig;

public class FiguraConfiguration : IEntityTypeConfiguration<Figura>
{
  public void Configure(EntityTypeBuilder<Figura> builder)
  {
    builder.ToTable("Figura");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => FiguraId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.Code)
      .HasConversion(
        vo => vo.Value,
        v => FiguraCode.From(v))
      .HasMaxLength(FiguraCode.MaxLength)
      .IsRequired();


    builder.Property(x => x.ApplicazioneId)
  .HasConversion(
    id => id.Value,
    value => ApplicazioneId.From(value))
  .IsRequired();

    builder.HasOne<Applicazione>()
  .WithMany()
  .HasForeignKey(x => x.ApplicazioneId)
  .OnDelete(DeleteBehavior.Restrict);


    builder.Property(x => x.Name)
      .HasConversion(
        vo => vo.Value,
        v => FiguraName.From(v))
      .HasMaxLength(FiguraName.MaxLength)
      .IsRequired();

    builder.HasIndex(x => x.Code).IsUnique();

    builder.Property(x => x.Description)
      .HasMaxLength(500);

    builder.HasMany(f => f.FiguraRuolos)
      .WithOne()
      .HasForeignKey(fr => fr.FiguraId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
