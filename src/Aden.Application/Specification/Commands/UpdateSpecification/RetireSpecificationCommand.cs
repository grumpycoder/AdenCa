using Aden.Application.Common.Exceptions;
using Aden.Infrastructure.Persistence;
using FluentValidation;
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
        var entity = await _context.Specifications.FindAsync(request.Id);

        if (entity == null) throw new NotFoundException(nameof(FileSpecification), request.Id);

        if (entity.IsRetired) throw new BadRequestException("Specification is already retired"); 
        
        entity.Retire();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}