using Aden.Domain;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;

namespace Aden.Application.FileSpecification.Commands.CreateFileSpecification;

public class CreateSpecificationCommand: IRequest<Specification>
{
    public string Filename { get; set; }
    public string FileNumber { get; set; }
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
}

public class CreateSpecificationCommandHandler: IRequestHandler<CreateSpecificationCommand, Specification>
{
    private readonly ApplicationDbContext _context;

    public CreateSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Specification> Handle(CreateSpecificationCommand request, CancellationToken cancellationToken)
    {
        var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch); 
        var entity = new Specification(request.FileNumber, request.Filename, reportLevel); 

        return entity; 
    }
}
