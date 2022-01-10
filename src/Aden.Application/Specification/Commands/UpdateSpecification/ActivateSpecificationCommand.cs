using Aden.Application.Common.Exceptions;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.UpdateFileSpecification;

public class ActivateSpecificationCommand: IRequest
{
    public ActivateSpecificationCommand(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
}

public class ActivateSpecificationCommandHandler: IRequestHandler<ActivateSpecificationCommand>
{
    private readonly ApplicationDbContext _context;

    public ActivateSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public  async Task<Unit> Handle(ActivateSpecificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);

        if (entity == null) throw new NotFoundException(nameof(FileSpecification), request.Id); 
        
        entity.Activate();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}