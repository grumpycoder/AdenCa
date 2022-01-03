using System.Reflection;
using Aden.WebUI.Domain;
using Aden.WebUI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    
    public DbSet<FileSpecification> FileSpecifications { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}