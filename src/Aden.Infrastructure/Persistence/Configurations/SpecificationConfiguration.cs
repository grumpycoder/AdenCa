using Aden.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aden.Infrastructure.Persistence.Configurations;

public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.ToTable("FileSpecifications", schema: "Aden");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("FileSpecificationId");
        
        builder.OwnsOne(p => p.ReportLevel,
            p =>
            {
                p.Property(pp => pp.IsSea).HasColumnName("IsSea");
                p.Property(pp => pp.IsLea).HasColumnName("IsLea");
                p.Property(pp => pp.IsSch).HasColumnName("IsSch");
            });
        
        var navigation = builder.Metadata.FindNavigation(nameof(Specification.Submissions));
        
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        
   
        
        /*builder.HasMany(x => x.Submissions)
            .WithOne()
            .IsRequired()
            // .HasForeignKey(x => x.SpecificationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Metadata
            .FindNavigation("Submissions")
            .SetPropertyAccessMode(PropertyAccessMode.Field);*/
        
    }
}