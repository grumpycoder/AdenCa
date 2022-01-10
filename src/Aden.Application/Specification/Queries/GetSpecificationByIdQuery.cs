using Aden.Application.Common.Exceptions;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.FileSpecification.Queries;
public class GetSpecificationByIdQuery: IRequest<Domain.Entities.Specification>
{
    public int Id { get; }

    public GetSpecificationByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetSpecificationByIdQueryHandler: IRequestHandler<GetSpecificationByIdQuery, Domain.Entities.Specification>
{
    private readonly ApplicationDbContext _context;

    public GetSpecificationByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Domain.Entities.Specification> Handle(GetSpecificationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications
            .Include(x => x.Submissions)
            .FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if(entity == null) throw new NotFoundException(nameof(Domain.Entities.Specification), request.Id);
        
        return entity; 
    }
}
