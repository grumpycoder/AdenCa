using Aden.Application.Common.Exceptions;
using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.Submission.Commands.CreateSubmission;

public class CreateSubmissionCommand: IRequest<Specification>
{
    public int SpecificationId { get; set; }
    public DateOnly DueDate { get; set; }
    public int DataYear { get; set; }
}

public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, Specification>
{
    private readonly ApplicationDbContext _context;

    public CreateSubmissionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Specification> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
    {
        var specification = await _context.Specifications
            .Include(x => x.Submissions)
            .FirstAsync(x => x.Id == request.SpecificationId, cancellationToken: cancellationToken);
        
        if(specification == null) throw new NotFoundException(nameof(Domain.Entities.Specification), request.SpecificationId);

        var submission = new Domain.Entities.Submission(request.DueDate, request.DataYear); 
        
        specification.AddSubmission(submission);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return specification; 
    }
}