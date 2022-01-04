using Aden.WebUI.Domain.ValueObjects;
using Aden.WebUI.Persistence;
using MediatR;

namespace Aden.WebUI.Application.FileSpecification.Commands;

public class CreateFileSpecificationCommand: IRequest<Domain.Entities.FileSpecification>
{
    public string Filename { get; set; }
    public string FileNumber { get; set; }
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
}

public class CreateFileSpecificationCommandHandler: IRequestHandler<CreateFileSpecificationCommand, Domain.Entities.FileSpecification>
{
    private readonly ApplicationDbContext _context;

    public CreateFileSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Domain.Entities.FileSpecification> Handle(CreateFileSpecificationCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.FileSpecification()
        {
            Id = 500,
            Filename = request.Filename,
            FileNumber = request.FileNumber,
            ReportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch)
        };

        return entity; 
    }
}