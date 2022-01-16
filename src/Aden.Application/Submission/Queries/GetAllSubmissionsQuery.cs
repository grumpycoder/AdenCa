using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.Submission.Queries;

public class GetAllSubmissionsQuery: IRequest<List<Domain.Entities.Submission>>
{
}

public class GetAllSubmissionsQueryHandler : IRequestHandler<GetAllSubmissionsQuery, List<Domain.Entities.Submission>>
{
    private readonly ApplicationDbContext _context;

    public GetAllSubmissionsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Domain.Entities.Submission>> Handle(GetAllSubmissionsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Submissions
            .Include(x => x.Specification)
            .ToListAsync(cancellationToken);

        return list; 
    }
}