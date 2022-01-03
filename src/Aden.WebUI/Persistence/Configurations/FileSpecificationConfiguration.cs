using Aden.WebUI.Domain;
using Aden.WebUI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aden.WebUI.Persistence.Configurations;

public class FileSpecificationConfiguration : IEntityTypeConfiguration<FileSpecification>
{
    public void Configure(EntityTypeBuilder<FileSpecification> builder)
    {
        builder.ToTable("FileSpecifications", schema: "Aden");
        builder.Property(p => p.Id).HasColumnName("FileSpecificationId");
        builder.OwnsOne(p => p.ReportLevel,
            p =>
            {
                p.Property(pp => pp.IsSea).HasColumnName("IsSea");
                p.Property(pp => pp.IsLea).HasColumnName("IsLea");
                p.Property(pp => pp.IsSch).HasColumnName("IsSch");
            });
    }
}