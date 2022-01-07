using Aden.Application.Common.Exceptions;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Queries;
public class GetSpecificationByIdQuery: IRequest<Specification>
{
    public int Id { get; }

    public GetSpecificationByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetSpecificationByIdQueryHandler: IRequestHandler<GetSpecificationByIdQuery, Specification>
{
    private readonly ApplicationDbContext _context;

    public GetSpecificationByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Specification> Handle(GetSpecificationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);
        if(entity == null) throw new NotFoundException(nameof(Specification), request.Id);
        
        return entity; 
    }
}
