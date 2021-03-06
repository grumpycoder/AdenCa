using Aden.Domain;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aden.Infrastructure.Persistence.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ToTable("Submissions", schema: "Aden");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("SubmissionId");

        builder.Property(p => p.DueDate).HasConversion<NullableDateOnlyConverter>()
            .HasColumnType("smalldatetime");
        
        builder
            .Property(e => e.SubmissionState)
            .HasColumnType("tinyint")
            // .HasConversion(
            //     v => v.ToString(),
            //     v => (SubmissionState)Enum.Parse(typeof(SubmissionState), v))
            .HasColumnName("SubmissionStateId")
            .HasConversion(x => (int) x, x => (SubmissionState) x);
            //.HasColumnType("tinyint")
            ;
        
        builder
            .HasOne<Specification>(e => e.Specification)
            .WithMany(g => g.Submissions)
            .HasForeignKey("FileSpecificationId");
        
    }
}

