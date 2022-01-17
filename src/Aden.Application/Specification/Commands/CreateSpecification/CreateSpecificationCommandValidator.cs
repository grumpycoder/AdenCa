using Aden.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Aden.Application.FileSpecification.Commands.CreateFileSpecification;

public class CreateSpecificationCommandValidator: AbstractValidator<CreateSpecificationCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateSpecificationCommandValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Filename)
            .NotEmpty().WithMessage("File name is required.")
            .MaximumLength(250).WithMessage("File name must not exceed 250 characters.");
        
        RuleFor(v => v.FileNumber)
            .NotEmpty().WithMessage("File Number is required.")
            .MaximumLength(3).WithMessage("File Number must not exceed 3 characters.")
            .MinimumLength(3).WithMessage("File Number must be at least 3 characters.")
            //.MustAsync(BeUniqueFileNumber).WithMessage("The specified file number already exists.");
            .Must(BeUniqueFileNumber).WithMessage("The specified file number already exists.");
    }

    private async Task<bool> BeUniqueFileNumber(string fileNumber, CancellationToken cancellationToken)
    {
        return await _context.Specifications
            .AllAsync(l => l.FileNumber != fileNumber, cancellationToken);
    }
    
    private bool BeUniqueFileNumber(string fileNumber)
    {
        return  _context.Specifications
            .All(l => l.FileNumber != fileNumber);
    }
}