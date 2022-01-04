using Aden.WebUI.Persistence;
using MediatR;

namespace Aden.WebUI.Application.FileSpecification.Commands;

public class UpdateFileSpecificationCommand: IRequest<string>, IRequest<Domain.Entities.FileSpecification>
{
    
}

public class UpdateFileSpecificationCommandHandler: IRequestHandler<UpdateFileSpecificationCommand, Domain.Entities.FileSpecification>
{
    private readonly ApplicationDbContext _context;

    public UpdateFileSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Domain.Entities.FileSpecification> Handle(UpdateFileSpecificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(1);
        return entity;
    }
}
