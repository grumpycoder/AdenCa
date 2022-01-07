using Aden.Domain;
using Aden.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aden.Infrastructure.Persistence.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ToTable("Submissions", schema: "Aden");
        builder.Property(p => p.Id).HasColumnName("SubmissionId");
        builder.Property(p => p.DueDate).HasConversion<NullableDateOnlyConverter>()
            .HasColumnType("smalldatetime");
    }
}

