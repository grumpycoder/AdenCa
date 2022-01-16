using Aden.Application.Common.Exceptions;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.Submission.Queries;

public class GetSubmissionByIdQuery: IRequest<Domain.Entities.Submission>
{
    public int Id { get; }

    public GetSubmissionByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetSubmissionByIdQueryHandler: IRequestHandler<GetSubmissionByIdQuery, Domain.Entities.Submission>
{
    private readonly ApplicationDbContext _context;

    public GetSubmissionByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Submission> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SubmissionsWithSpecification(request.Id);
        
        if (entity == null) throw new NotFoundException(nameof(Submission), request.Id);

        return entity; 
    }
}
