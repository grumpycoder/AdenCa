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

    public async Task<Unit> Handle(UpdateSpecificationCommand request, CancellationToken token)
    {
        var entity = await _context.Specifications.FindAsync(request.Id);

        if (entity == null)throw new NotFoundException(nameof(FileSpecification), request.Id);

        var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch);

        entity.Rename(request.FileNumber, request.Filename);
        entity.UpdateReportProcessDetail(reportLevel, request.FilenameFormat, request.ReportAction);

        entity.UpdateApplicationDetails(request.Application, request.SupportGroup, 
            request.Collection, request.Section, request.SpecificationUrl);

        if (request.IsRetired != entity.IsRetired && request.IsRetired) entity.Retire();
        if (request.IsRetired != entity.IsRetired && !request.IsRetired) entity.Activate();
        
        await _context.SaveChangesAsync(token);
        
        return Unit.Value;
    }
}