using DirectoryService.Domain.Department;
using DirectoryService.Domain.Department.VO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(x => x.Id) // primy key
               .HasName("pk_departments");

        builder.Property(d => d.Id)
            .HasConversion(d => d.Value, id => DepartmentId.Create(id))
            .HasColumnName("id");

        builder.ComplexProperty(d => d.Name, nb =>
        {
            nb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(DepartmentName.MAX_LENGTH)
                .HasColumnName("name");
        });

        builder.ComplexProperty(d => d.DepartmentIdentifier, ib =>
        {
            ib.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(DepartmentIdentifier.MAX_LENGTH)
                .HasColumnName("identifier");
        });

        builder.ComplexProperty(d => d.Path, pb =>
        {
            pb.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("path");
        });

        builder.Property(d => d.ParentId)
            .IsRequired(false)
            .HasColumnName("parent_id");

        builder.Property(d => d.Depth)
            .IsRequired()
            .HasColumnName("depth");

        builder.Property(d => d.IsActive)
            .HasColumnName("active");

        builder.Property(d => d.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(d => d.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder
            .HasMany(d => d.ChildDepartments)
            .WithOne()
            .HasForeignKey(cd => cd.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}