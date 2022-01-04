using Aden.WebUI.Domain.ValueObjects;
using MediatR;

namespace Aden.WebUI.Domain.Entities;

public class FileSpecification : IRequest<Unit>
{
    public int Id { get; set; }
    public string FileNumber { get; set; }
    public string Filename { get; set; }
    public ReportLevel ReportLevel { get; set; }
}