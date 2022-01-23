using Aden.Application.Interfaces;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;

namespace Aden.Infrastructure.Repositories;

public class SpecificationRepository: Repository<Specification>, ISpecificationRepository
{
    public SpecificationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Specification> GetSpecificationWithSubmissions(int id)
    {
        return await Context.SpecificationWithSubmissions(id);
    }
}