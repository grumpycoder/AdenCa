using Aden.Application.Interfaces;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;

namespace Aden.Infrastructure.Repositories;

public class SubmissionRepository: Repository<Submission>, ISubmissionRepository
{
    public SubmissionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Submission> GetSubmissionWithSpecification(int id)
    {
        return await Context.SubmissionsWithSpecification(id);
    }
}