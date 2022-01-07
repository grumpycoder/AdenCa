using Aden.Infrastructure.Persistence;
using FluentValidation;

namespace Aden.Application.FileSpecification.Commands.UpdateFileSpecification;

public class UpdateSpecificationCommandValidator: AbstractValidator<UpdateSpecificationCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateSpecificationCommandValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Filename)
            .NotEmpty().WithMessage("File name is required.")
            .MaximumLength(250).WithMessage("File name must not exceed 250 characters.");
        
        RuleFor(v => v.FileNumber)
            .NotEmpty().WithMessage("File Number is required.")
            .MaximumLength(3).WithMessage("File Number must not exceed 3 characters.")
            .MinimumLength(3).WithMessage("File Number must be at least 3 characters.")
            //.MustAsync(BeUniqueFileNumber).WithMessage("The specified file number already exists.")
            //.Must(BeUniqueFileNumber).WithMessage("The specified file number already exists")
            .Must((model, fileNumber) => BeUniqueFileNumber(model.Id, fileNumber)).WithMessage($"The specified file number already exists");
    }
    
    private bool BeUniqueFileNumber(int id, string fileNumber)
    {
        return _context.FileSpecifications.Any(x => x.FileNumber == fileNumber && x.Id == id) || _context.FileSpecifications.All(l => l.FileNumber != fileNumber);
    }
    
    // private async Task<bool> BeUniqueFileNumber(string fileNumber, CancellationToken cancellationToken)
    // {
    //     return await _context.FileSpecifications
    //         .AllAsync(l => l.FileNumber != fileNumber, cancellationToken);
    // }
    //
    // private bool BeUniqueFileNumber(string fileNumber)
    // {
    //     return  _context.FileSpecifications
    //         .All(l => l.FileNumber != fileNumber);
    // }
    //
    // private bool BeUniqueFileNumber2(string fileNumber, int id)
    // {
    //     return  _context.FileSpecifications
    //         .All(l => l.FileNumber != fileNumber);
    // }
}