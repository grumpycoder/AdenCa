using Aden.WebUI.Application.Common.Exceptions;
using Aden.WebUI.Persistence;
using MediatR;

namespace Aden.WebUI.Application.FileSpecification.Commands.UpdateFileSpecification;

public class UpdateFileSpecificationCommand: IRequest, IRequest<int>
{
    public int Id { get; set; }
    public string Filename { get; set; }
    public string FileNumber { get; set; }
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
}

public class UpdateFileSpecificationCommandHandler: IRequestHandler<UpdateFileSpecificationCommand, int>
{
    private readonly ApplicationDbContext _context;

    public UpdateFileSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> Handle(UpdateFileSpecificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);
        
        throw new NotFoundException(nameof(FileSpecification), request.Id);
    }
}