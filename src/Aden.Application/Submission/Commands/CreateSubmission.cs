using Aden.Application.Common.Exceptions;
using Aden.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Aden.Application.Submission.Commands;

public static class CreateSubmission
{
    public record Command(int SpecificationId, DateOnly DueDate, int DataYear) : IRequest<Response>;

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var specification = await _uow.Specifications.GetSpecificationWithSubmissions(request.SpecificationId);

            if (specification == null) throw new NotFoundException(nameof(Specification), request.SpecificationId);

            var submission = new Aden.Domain.Entities.Submission(request.DueDate, request.DataYear);

            specification.AddSubmission(submission);

            _uow.Complete(); 

            return new Response()
            {
                Id = submission.Id, 
                DueDate = submission.DueDate, 
                SubmissionState = submission.SubmissionState.ToString(), 
                Specification = specification 
            };
        }
    }

     public class Validator : AbstractValidator<Command>
     {
         private readonly IUnitOfWork _uow;
        
         public Validator(IUnitOfWork uow)
         {
             _uow = uow;
             RuleFor(v => v.DataYear)
                 .NotEmpty().WithMessage("Data Year is required")
                 .GreaterThan(2000)
                 .Must((model, dataYear) => BeUniqueDataYear(model.SpecificationId, dataYear))
                 .WithMessage("Submission with Data year already exists.")
                 .GreaterThanOrEqualTo(x => x.DueDate.Year).WithMessage("Data Year must be less than or equal to Due Date year");
        
             RuleFor(v => v.DueDate)
                 .NotEmpty().WithMessage("Due Date is required")
                 .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                 .WithMessage($"The specified Due Date cannot be in the past.");
         }
        
         private bool BeUniqueDataYear(int specificationId, int dataYear)
         {
             var specification = _uow.Specifications.GetSpecificationWithSubmissions((specificationId)).Result;
             return specification.Submissions.All(x => x.DataYear != dataYear);
         }
     }

    public class Response
    {
        public int Id { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int DataYear { get; set; }

        public string SubmissionState { get; set; }
        public Domain.Entities.Specification Specification { get; set; }
    }
}