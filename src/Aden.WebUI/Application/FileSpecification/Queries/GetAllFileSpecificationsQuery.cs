using Aden.WebUI.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI.Application.FileSpecification.Queries;

public class GetAllFileSpecificationsQuery: IRequest<List<Domain.Entities.FileSpecification>>
{
    
}

public class GetAllFileSpecificationsQueryHandler : IRequestHandler<GetAllFileSpecificationsQuery, List<Domain.Entities.FileSpecification>>
{
    private readonly ApplicationDbContext _context;
    public GetAllFileSpecificationsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Domain.Entities.FileSpecification>> Handle(GetAllFileSpecificationsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.FileSpecifications.AsNoTracking().ToListAsync(cancellationToken);
        return list; 
    }
}
