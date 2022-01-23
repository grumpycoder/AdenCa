using Aden.Application.Common.Exceptions;
using Aden.Application.Interfaces;
using Aden.Domain;
using FluentValidation;
using MediatR;

namespace Aden.Application.Specification.Commands;

public static class UpdateSpecification
{
    public record Command(int Id, string FileName, string FileNumber, bool IsSea, bool IsLea, bool IsSch, 
        bool IsRetired, string Section, string Application, string SupportGroup, string Collection, 
        string SpecificationUrl, string FileNameFormat, string ReportAction) : IRequest
    {
    }

    // public class Validator: AbstractValidator<Command>
    // {
        //TODO: Refactor Validator
        // private readonly ApplicationDbContext _context;
        //
        // public Validator(ApplicationDbContext context)
        // {
        //     _context = context;
        //
        //     RuleFor(v => v.FileName)
        //         .NotEmpty().WithMessage("File name is required.")
        //         .MaximumLength(250).WithMessage("File name must not exceed 250 characters.");
        //
        //     RuleFor(v => v.FileNumber)
        //         .NotEmpty().WithMessage("File Number is required.")
        //         .MaximumLength(3).WithMessage("File Number must not exceed 3 characters.")
        //         .MinimumLength(3).WithMessage("File Number must be at least 3 characters.")
        //         //.MustAsync(BeUniqueFileNumber).WithMessage("The specified file number already exists.")
        //         //.Must(BeUniqueFileNumber).WithMessage("The specified file number already exists")
        //         .Must((model, fileNumber) => BeUniqueFileNumber(model.Id, fileNumber)).WithMessage($"The specified file number already exists");
        // }
        //
        // private bool BeUniqueFileNumber(int id, string fileNumber)
        // {
        //     return _context.Specifications.Any(x => x.FileNumber == fileNumber && x.Id == id) || _context.Specifications.All(l => l.FileNumber != fileNumber);
        // }
    
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
    // }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(Command request, CancellationToken token)
        {
            var entity = await _uow.Specifications.GetSpecificationWithSubmissions(request.Id);

            if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Specification), request.Id);

            var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch);

            entity.Rename(request.FileNumber, request.FileName);
            entity.UpdateReportProcessDetail(reportLevel, request.FileNameFormat, request.ReportAction);

            entity.UpdateApplicationDetails(request.Application, request.SupportGroup,
                request.Collection, request.Section, request.SpecificationUrl);

            if (request.IsRetired != entity.IsRetired && request.IsRetired) entity.Retire();
            if (request.IsRetired != entity.IsRetired && !request.IsRetired) entity.Activate();

            _uow.Complete();
            return Unit.Value;
        }
    }
}