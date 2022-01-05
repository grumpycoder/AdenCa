using Aden.WebUI.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI.Application.FileSpecification.Commands.CreateFileSpecification;

public class CreateFileSpecificationCommandValidator: AbstractValidator<CreateFileSpecificationCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateFileSpecificationCommandValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.FileNumber)
            .NotEmpty().WithMessage("File Number is required.")
            .MaximumLength(3).WithMessage("File Number must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified file number already exists.");
    }

    private async Task<bool> BeUniqueTitle(string fileNumber, CancellationToken cancellationToken)
    {
        return await _context.FileSpecifications
            .AllAsync(l => l.FileNumber != fileNumber, cancellationToken);
    }
}