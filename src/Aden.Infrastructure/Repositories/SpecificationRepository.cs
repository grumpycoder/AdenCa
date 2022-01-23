using Aden.Application.Interfaces;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Aden.Infrastructure.Repositories;

public class SpecificationRepository : Repository<Specification>, ISpecificationRepository
{
    public SpecificationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Specification> GetSpecificationWithSubmissions(int id)
    {
        return await Context.SpecificationWithSubmissions(id);
    }

    public bool GetUniqueFileNumbers(string fileNumber)
    {
        return Context.Specifications
            .All(l => l.FileNumber != fileNumber);
    }

    public async Task<bool> GetUniqueFileNumbersAsync(string fileNumber)
    {
        return await Context.Specifications
            .AllAsync(l => l.FileNumber != fileNumber);
    }

    public bool GetUniqueFileNumbers(int id, string fileNumber)
    {
        return Context.Specifications
                   .Any(x => x.FileNumber == fileNumber && x.Id == id) ||
               Context.Specifications.All(l => l.FileNumber != fileNumber);
    }

    public async Task<bool> GetUniqueFileNumbersAsync(int id, string fileNumber)
    {
        return await Context.Specifications
                   .AnyAsync(x => x.FileNumber == fileNumber && x.Id == id) ||
               Context.Specifications.All(l => l.FileNumber != fileNumber);
    }
}