using Aden.Application.Interfaces;
using Aden.Domain.Entities;
using MediatR;

namespace Aden.Application.Submission.Queries;

public static class GetAllSubmissions
{
    public record Query() : IRequest<List<Response>>;

    public class Handler : IRequestHandler<Query, List<Response>>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var list = await _uow.Submissions.All(); 
            
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
        public Domain.Entities.Specification Specification { get; set; }
    }
}