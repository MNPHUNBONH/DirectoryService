using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(x => x.Id)
            .HasName("pk_positions");

        builder.Property(d => d.Id)
            .HasConversion(d => d.Value, id => PositionId.Create(id))
            .HasColumnName("id");

        builder.ComplexProperty(p => p.Name, pn =>
        {
            pn.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(PositionName.MAX_LENGTH)
                .HasColumnName("name");
        });

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasColumnName("description")
            .HasMaxLength(1000);

        builder.Property(p => p.IsActive)
            .HasColumnName("active");

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(p => p.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");
    }
}