using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.FileSpecification.Queries;

public class GetAllFileSpecificationsQuery: IRequest<List<FileSpec>>
{
    
}

public class GetAllFileSpecificationsQueryHandler : IRequestHandler<GetAllFileSpecificationsQuery, List<FileSpec>>
{
    private readonly ApplicationDbContext _context;
    public GetAllFileSpecificationsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<FileSpec>> Handle(GetAllFileSpecificationsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.FileSpecifications.AsNoTracking().ToListAsync(cancellationToken);
        return list; 
    }
}
