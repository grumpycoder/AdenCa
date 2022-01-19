using Aden.Application.Common.Exceptions;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.Submission.Commands.CreateSubmission;

public class CreateSubmissionCommand: IRequest<Domain.Entities.Specification>
{
    public int SpecificationId { get; set; }
    public DateOnly DueDate { get; set; }
    public int DataYear { get; set; }
}

public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, Domain.Entities.Specification>
{
    private readonly ApplicationDbContext _context;

    public CreateSubmissionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Specification> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
    {
        var specification = await _context.Specifications
            .Include(x => x.Submissions)
            .FirstAsync(x => x.Id == request.SpecificationId, cancellationToken: cancellationToken);
        
        if(specification == null) throw new NotFoundException(nameof(Specification), request.SpecificationId);

        var submission = new Aden.Domain.Entities.Submission(request.DueDate, request.DataYear); 
        
        specification.AddSubmission(submission);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return specification; 
    }
}