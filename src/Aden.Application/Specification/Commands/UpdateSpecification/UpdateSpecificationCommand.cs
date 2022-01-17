using Aden.Application.Common.Exceptions;
using Aden.Domain;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.UpdateFileSpecification;

public class UpdateSpecificationCommand : IRequest
{
    public int Id { get; set; }
    public string Filename { get; init; }
    public string FileNumber { get; set; }
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
    public bool IsRetired { get; set; }

    public string? Section { get;  set; }
    public string? Application { get;  set; }
    public string? SupportGroup { get;  set; }
    public string? Collection { get;  set; }
    public string? SpecificationUrl { get;  set; }
    
    public string? FilenameFormat { get;  set; }
    public string? ReportAction { get;  set; }
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
        var entity = await _context.Specifications.FindAsync(request.Id);
        if (entity == null)
        {
            throw new NotFoundException(nameof(FileSpecification), request.Id);
        }

        var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch); 
        entity.Update(request.FileNumber, request.Filename, reportLevel, request.Application, 
            request.SupportGroup, request.Collection, request.SpecificationUrl, request.FilenameFormat, 
            request.ReportAction);

        if(request.IsRetired) entity.Retire();

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}