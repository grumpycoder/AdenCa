using Aden.Application.Common.Exceptions;
using Aden.Application.Submission.Queries;
using Aden.Infrastructure.Persistence;
using Aden.SharedKernal;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Aden.Application.Submission.Commands;

public static class CreateSubmission
{
    public record Command(int SpecificationId, DateOnly DueDate, int DataYear) : IRequest<Response>;

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var specification = await _context.Specifications
                .Include(x => x.Submissions)
                .FirstAsync(x => x.Id == request.SpecificationId, cancellationToken: cancellationToken);

            if (specification == null) throw new NotFoundException(nameof(Specification), request.SpecificationId);

            var submission = new Aden.Domain.Entities.Submission(request.DueDate, request.DataYear);

            specification.AddSubmission(submission);

            await _context.SaveChangesAsync(cancellationToken);

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
        private readonly ApplicationDbContext _context;

        public Validator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.DataYear)
                .NotEmpty().WithMessage("Data Year is required")
                .GreaterThan(2000)
                .Must((model, dataYear) => BeUniqueDataYear(model.SpecificationId, dataYear))
                .WithMessage("Submission with Data year already exists.")
                .GreaterThanOrEqualTo(x => x.DueDate.Year).WithMessage("Data Year must be less than or equal to Due Date year");

            RuleFor(v => v.DueDate)
                .NotEmpty().WithMessage("Due Date is required")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage($"The specified Due Date cannot be in the past.")
                //.GreaterThanOrEqualTo(x => x.DueDate.Year)
                ;
            
            
        }

        private bool BeUniqueDataYear(int specificationId, int dataYear)
        {
            var specification = _context.SpecificationWithSubmissions(specificationId).Result;
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