using Aden.Infrastructure.Persistence;
using FluentValidation;

namespace Aden.Application.Submission.Commands.CreateSubmission;

public class CreateSubmissionCommandValidator: AbstractValidator<CreateSubmissionCommand>
{
    private readonly ApplicationDbContext _context;


    public CreateSubmissionCommandValidator(ApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.DataYear)
            .NotEmpty().WithMessage("Data Year is required")
            .GreaterThan(2000)
            .Must((model, dataYear) => BeUniqueDataYear(model.SpecificationId, dataYear)).WithMessage("Submission with Data year already exists.");

        RuleFor(v => v.DueDate)
            .NotEmpty().WithMessage("Due Date is required")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage($"The specified Due Date cannot be in the past.")
            ;
    }

    private bool BeUniqueDataYear(int specificationId, int dataYear)
    {
        var specification = _context.SpecificationWithSubmissions(specificationId).Result;
        return specification.Submissions.All(x => x.DataYear != dataYear);
    }

}