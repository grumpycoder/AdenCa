using Aden.Application.Common.Exceptions;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Queries;
public class GetFileSpecificationByIdQuery: IRequest<FileSpec>
{
    public int Id { get; }

    public GetFileSpecificationByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetFileSpecificationByIdQueryHandler: IRequestHandler<GetFileSpecificationByIdQuery, FileSpec>
{
    private readonly ApplicationDbContext _context;

    public GetFileSpecificationByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<FileSpec> Handle(GetFileSpecificationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);
        if(entity == null) throw new NotFoundException(nameof(FileSpec), request.Id);
        
        return entity; 
    }
}
