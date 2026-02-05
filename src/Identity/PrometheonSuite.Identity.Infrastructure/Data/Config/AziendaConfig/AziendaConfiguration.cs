using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.AziendaAggregate;


namespace PrometheonSuite.Identity.Infrastructure.Data.Config.AziendaConfig;

public class AziendaConfiguration : IEntityTypeConfiguration<Azienda>
{
  public void Configure(EntityTypeBuilder<Azienda> builder)
  {
    builder.ToTable("Azienda");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => AziendaId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.Name)
      .HasConversion(
        vo => vo.Value,
        v => AziendaName.From(v))
      .HasMaxLength(AziendaName.MaxLength)
      .IsRequired();

    builder.Property(x => x.Code)
      .HasConversion(
        vo => vo.Value,
        v => AziendaCode.From(v))
      .HasMaxLength(AziendaCode.MaxLength)
      .IsRequired();

    builder.HasIndex(x => x.Code).IsUnique();

    builder.Property(x => x.IsActive)
      .IsRequired();
  }
}
