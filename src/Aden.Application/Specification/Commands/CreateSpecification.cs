using Aden.Application.Interfaces;
using Aden.Domain;
using FluentValidation;
using MediatR;

namespace Aden.Application.Specification.Commands;

public static class CreateSpecification
{
    public record Command(string Filename, string FileNumber, bool IsSea, bool IsLea, bool IsSch, string Section,
        string Application, string SupportGroup, string Collection, string SpecificationUrl, string FilenameFormat,
        string ReportAction) : IRequest<Response>;

    public class Handler: IRequestHandler<Command, Response>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Response> Handle(Command request, CancellationToken token)
        {
            var reportLevel = new ReportLevel(request.IsSea, request.IsLea, request.IsSch); 
            var entity = new Domain.Entities.Specification(request.FileNumber, request.Filename, reportLevel); 

            entity.UpdateApplicationDetails(request.Application, request.SupportGroup, request.Collection, 
                request.Section, request.SpecificationUrl);

            entity.UpdateReportProcessDetail(reportLevel, request.FilenameFormat, request.ReportAction);
        
            _uow.Specifications.Add(entity);
            _uow.Complete();
        
            return new Response()
            {
                Id = entity.Id, 
                FileNumber = entity.FileNumber, 
                FileName = entity.FileName, 
                ReportLevel = entity.ReportLevel, 
                Section = entity.Section, 
                Application = entity.Application, 
                SupportGroup = entity.SupportGroup, 
                Collection = entity.Collection, 
                SpecificationUrl = entity.SpecificationUrl, 
                FilenameFormat = entity.FilenameFormat, 
                ReportAction = entity.ReportAction, 
                IsRetired = entity.IsRetired 
            };
            
             
        }
    }

    // public class Validator: AbstractValidator<Command>
    // {
        //TODO: Refactor Validator
       //  private readonly ApplicationDbContext _context;
       //
       //  public Validator(ApplicationDbContext context)
       //  {
       //      _context = context;
       //
       //      RuleFor(v => v.Filename)
       //          .NotEmpty().WithMessage("File name is required.")
       //          .MaximumLength(250).WithMessage("File name must not exceed 250 characters.");
       //  
       //      RuleFor(v => v.FileNumber)
       //          .NotEmpty().WithMessage("File Number is required.")
       //          .MaximumLength(3).WithMessage("File Number must not exceed 3 characters.")
       //          .MinimumLength(3).WithMessage("File Number must be at least 3 characters.")
       //          //.MustAsync(BeUniqueFileNumber).WithMessage("The specified file number already exists.");
       //          .Must(BeUniqueFileNumber).WithMessage("The specified file number already exists.");
       //      
       // }

        // private async Task<bool> BeUniqueFileNumber(string fileNumber, CancellationToken cancellationToken)
        // {
        //     return await _context.Specifications
        //         .AllAsync(l => l.FileNumber != fileNumber, cancellationToken);
        // }
    
        // private bool BeUniqueFileNumber(string fileNumber)
        // {
        //     return  _context.Specifications
        //         .All(l => l.FileNumber != fileNumber);
        // }
    // }
    
    public class Response
    {
        public int Id { get; set; }
        public string FileNumber { get;  set; }
        public string FileName { get;  set; }
        public ReportLevel ReportLevel { get;  set; }

        public string Section { get;  set; }
        public string Application { get;  set; }
        public string SupportGroup { get;  set; }
        public string Collection { get;  set; }
        public string SpecificationUrl { get;  set; }

        public string FilenameFormat { get;  set; }
        public string ReportAction { get;  set; }

        public bool? IsRetired { get; set; }

    }
}

