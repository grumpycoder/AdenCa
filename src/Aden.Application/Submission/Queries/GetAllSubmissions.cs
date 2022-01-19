using Aden.Domain.Entities;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.Submission.Queries;

public static class GetAllSubmissions
{
    public record Query() : IRequest<List<Response>>;

    public class Handler : IRequestHandler<Query, List<Response>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var list = await _context.Submissions
                .Include(x => x.Specification)
                .ToListAsync(cancellationToken);

            var response = new List<Response>();
            foreach (var entity in list)
            {
                response.Add(new Response()
                {
                    DueDate = entity.DueDate,
                    SubmissionDate = entity.SubmissionDate,
                    DataYear = entity.DataYear,
                    SubmissionState = entity.SubmissionState,
                    Specification = entity.Specification
                });
            }

            return response;
        }
    }

    public class Response
    {
        public DateOnly? DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int DataYear { get; set; }

        public SubmissionState SubmissionState { get; set; }
        public Specification Specification { get; set; }
    }
}