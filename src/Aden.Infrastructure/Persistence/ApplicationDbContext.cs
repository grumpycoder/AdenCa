using System.Reflection;
using Aden.Domain;
using Aden.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aden.Infrastructure.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    
    public DbSet<Specification> FileSpecifications { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        // builder.Entity<Submission>()
        //     .HasOne<Specification>(e => e.Specification)
        //     .WithMany(g => g.Submissions)
        //     .HasForeignKey("FileSpecificationId");
        
    }
    
}