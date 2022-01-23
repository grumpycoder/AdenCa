using Aden.Application.Common.Exceptions;
using Aden.Application.Interfaces;
using Aden.Domain.Entities;
using MediatR;

namespace Aden.Application.Submission.Queries;

public static class GetSubmissionById
{
    public record Query(int Id) : IRequest<Response>; 
    
    public class Handler: IRequestHandler<Query, Response>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Submissions.GetSubmissionWithSpecification(request.Id); 
            
            if (entity == null) throw new NotFoundException(nameof(Submission), request.Id);

            return new Response()
            {
                DueDate = entity.DueDate, 
                SubmissionDate = entity.SubmissionDate, 
                DataYear = entity.DataYear, 
                SubmissionState = entity.SubmissionState, 
                Specification = entity.Specification
            };
        }
    }
    
    public class Response
    {
        public DateOnly? DueDate { get;  set; }
        public DateTime? SubmissionDate { get;  set; }
        public int DataYear { get;  set; }

        public SubmissionState SubmissionState { get;  set; }
        public Domain.Entities.Specification Specification { get;  set; }
    }
}
