using System.Reflection;
using Aden.Domain;
using Aden.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aden.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Specification> FileSpecifications { get; set; }
    public DbSet<Submission> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public static class ApplicationDbContextExtensions
{
    public static async Task<Specification> SpecificationWithSubmissions(this ApplicationDbContext context,
        int specificationId)
    {
        return await context.FileSpecifications
            .Include(s => s.Submissions)
            .FirstOrDefaultAsync(x => x.Id == specificationId)
            ;
    }
    
    public static async Task<Submission> SubmissionsWithSpecification(this ApplicationDbContext context,
        int submissionId)
    {
        return await context.Submissions
                .Include(s => s.Specification)
                .FirstOrDefaultAsync(x => x.Id == submissionId)
            ;
    }
}