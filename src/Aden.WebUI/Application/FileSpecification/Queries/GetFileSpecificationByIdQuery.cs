using Aden.WebUI.Persistence;
using MediatR;

namespace Aden.WebUI.Application.FileSpecification.Queries;

public class GetFileSpecificationByIdQuery: IRequest<Domain.Entities.FileSpecification>
{
    public int Id { get; }

    public GetFileSpecificationByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetFileSpecificationByIdQueryHandler: IRequestHandler<GetFileSpecificationByIdQuery, Domain.Entities.FileSpecification>
{
    private readonly ApplicationDbContext _context;

    public GetFileSpecificationByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Domain.Entities.FileSpecification> Handle(GetFileSpecificationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);
        return entity; 
    }
}