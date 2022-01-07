using Aden.Domain;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.CreateFileSpecification;

public class CreateFileSpecificationCommand: IRequest<FileSpec>
{
    public string Filename { get; set; }
    public string FileNumber { get; set; }
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
}

public class CreateFileSpecificationCommandHandler: IRequestHandler<CreateFileSpecificationCommand, FileSpec>
{
    private readonly ApplicationDbContext _context;

    public CreateFileSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<FileSpec> Handle(CreateFileSpecificationCommand request, CancellationToken cancellationToken)
    {
        var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch); 
        var entity = new FileSpec(request.FileNumber, request.Filename, reportLevel); 

        return entity; 
    }
}