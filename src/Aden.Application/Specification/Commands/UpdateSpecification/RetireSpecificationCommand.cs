using Aden.Application.Common.Exceptions;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.UpdateFileSpecification;

public class RetireSpecificationCommand: IRequest
{
    public RetireSpecificationCommand(int id)
    {
        Id = id; 
    }

    public int Id { get; init; }
}

public class RetireSpecificationCommandHandler: IRequestHandler<RetireSpecificationCommand>
{
    private readonly ApplicationDbContext _context;

    public RetireSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(RetireSpecificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);

        if (entity == null) throw new NotFoundException(nameof(FileSpecification), request.Id); 
        
        entity.Retire();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}