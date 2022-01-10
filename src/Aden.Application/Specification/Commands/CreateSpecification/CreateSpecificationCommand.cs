using Aden.Domain;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.CreateFileSpecification;

public class CreateSpecificationCommand: IRequest<Domain.Entities.Specification>
{
    public string Filename { get; set; }
    public string FileNumber { get; set; }
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

public class CreateSpecificationCommandHandler: IRequestHandler<CreateSpecificationCommand, Domain.Entities.Specification>
{
    private readonly ApplicationDbContext _context;

    public CreateSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Domain.Entities.Specification> Handle(CreateSpecificationCommand request, CancellationToken cancellationToken)
    {
        var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch); 
        var entity = new Domain.Entities.Specification(request.FileNumber, request.Filename, reportLevel); 

        entity.Update(request.FileNumber, request.Filename, reportLevel, request.Application, 
            request.SupportGroup, request.Collection, request.SpecificationUrl, request.FilenameFormat, 
            request.ReportAction);
        return entity; 
    }
}
