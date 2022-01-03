using Aden.WebUI.ValueObjects;

namespace Aden.WebUI.Domain.Entities;

public class FileSpecification
{
    public int Id { get; set; }
    public string FileNumber { get; set; }
    public string Filename { get; set; }
    public ReportLevel ReportLevel { get; set; }
}