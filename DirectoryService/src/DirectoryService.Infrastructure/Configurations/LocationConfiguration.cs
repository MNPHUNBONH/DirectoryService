using DirectoryService.Domain;
using DirectoryService.Domain.VO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(x => x.Id) // primy key
            .HasName("pk_locations");

        builder.Property(d => d.Id)
            .HasConversion(d => d.Value, id => LocationId.Create(id))
            .HasColumnName("id");

        builder.ComplexProperty(l => l.Name, nb =>
        {
            nb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(LocationName.MAX_LENGTH)
                .HasColumnName("name");
        });

        builder.ComplexProperty(l => l.Timezone, ib =>
        {
            ib.Property(l => l.Value)
                .IsRequired()
                .HasColumnName("timezone");
        });

        builder.Property(l => l.IsActive)
            .HasColumnName("active");

        builder.Property(l => l.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(l => l.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.OwnsMany(l => l.Address, lab =>
        {
            lab.ToJson("addresses");

            lab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(LocationAddress.MAX_LENGTH)
                .HasColumnName("city");

            lab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(LocationAddress.MAX_LENGTH)
                .HasColumnName("street");

            lab.Property(a => a.HouseNumber)
                .IsRequired()
                .HasMaxLength(LocationAddress.MAX_LENGTH)
                .HasColumnName("house_number");

        });

    }
}