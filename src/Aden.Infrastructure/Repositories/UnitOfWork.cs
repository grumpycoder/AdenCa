using Aden.Application.Interfaces;
using Aden.Infrastructure.Persistence;

namespace Aden.Infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, ISpecificationRepository specifications, ISubmissionRepository submissions)
    {
        _context = context;
        Specifications = specifications;
        Submissions = submissions;
    }
    
    public ISpecificationRepository Specifications { get; private set; }
    public ISubmissionRepository Submissions { get; private set;  }
    
    public int Complete()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}