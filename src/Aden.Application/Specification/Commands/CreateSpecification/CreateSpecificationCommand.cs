using Aden.Domain;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.CreateFileSpecification;

public class CreateSpecificationCommand: IRequest<Specification>
{
    public string Filename { get; init; } = string.Empty; 
    public string FileNumber { get; set; }= string.Empty;
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
    
    public string? Section { get;  set; }
    public string? Application { get;  set; }
    public string? SupportGroup { get;  set; }
    public string? Collection { get;  set; }
    public string? SpecificationUrl { get;  set; }
    
    public string? FilenameFormat { get;  set; }
    public string? ReportAction { get;  set; }
}

public class CreateSpecificationCommandHandler: IRequestHandler<CreateSpecificationCommand, Specification>
{
    private readonly ApplicationDbContext _context;

    public CreateSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Specification> Handle(CreateSpecificationCommand request, CancellationToken token)
    {
        var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch); 
        var entity = new Specification(request.FileNumber, request.Filename, reportLevel); 

        entity.UpdateApplicationDetails(request.Application, request.SupportGroup, request.Collection, 
            request.SpecificationUrl);

        entity.UpdateReportProcessDetail(reportLevel, request.FilenameFormat);
        
        _context.Specifications.Add(entity);
        await _context.SaveChangesAsync(token);
        
        return entity; 
    }
}
