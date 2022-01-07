using Aden.Domain;
using Aden.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aden.Infrastructure.Persistence.Configurations;

public class FileSpecificationConfiguration : IEntityTypeConfiguration<FileSpec>
{
    public void Configure(EntityTypeBuilder<FileSpec> builder)
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