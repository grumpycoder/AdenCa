using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.FileSpecification.Queries;

public class GetAllSpecificationsQuery: IRequest<List<Domain.Entities.Specification>>
{
    
}

public class GetAllSpecificationsQueryHandler : IRequestHandler<GetAllSpecificationsQuery, List<Domain.Entities.Specification>>
{
    private readonly ApplicationDbContext _context;
    public GetAllSpecificationsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Domain.Entities.Specification>> Handle(GetAllSpecificationsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Specifications
            .Include(x => x.Submissions)
            .AsNoTracking().ToListAsync(cancellationToken);
         
        return list; 
    }
}
