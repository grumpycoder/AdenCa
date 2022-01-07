using Aden.Application.Common.Exceptions;
using Aden.Domain;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.UpdateFileSpecification;

public class UpdateSpecificationCommand : IRequest
{
    public int Id { get; set; }
    public string Filename { get; set; }
    public string FileNumber { get; set; }
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
    public bool IsRetired { get; set; }
}

public class UpdateSpecificationCommandHandler : IRequestHandler<UpdateSpecificationCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSpecificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);
        if (entity == null)
        {
            throw new NotFoundException(nameof(FileSpecification), request.Id);
        }

        var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch); 
        entity.Update(request.FileNumber, request.FileNumber, reportLevel);

        if(request.IsRetired) entity.Retire();
        
        return Unit.Value;
    }
}