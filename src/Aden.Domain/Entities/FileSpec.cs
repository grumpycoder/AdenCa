namespace Aden.Domain.Entities;

public class FileSpec
{
    private FileSpec()
    {
    }

    public FileSpec(string fileNumber, string filename, ReportLevel reportLevel)
    {
        FileNumber = fileNumber;
        Filename = filename;
        ReportLevel = reportLevel;
    }

    public int Id { get; private set; }
    public string? FileNumber { get; private set; }
    public string? Filename { get; private set; }
    public ReportLevel? ReportLevel { get; private set; }

    public string? Section => null;

    public string? Application { get; private set; }
    public string? SupportGroup { get; private set; }
    public string? Collection { get; private set; }
    public string? SpecificationUrl { get; private set; }

    public bool IsRetired { get; set; }

    public void Update(string fileNumber, string fileName, ReportLevel reportLevel)
    {
        Filename = fileName;
        FileNumber = fileNumber;
        ReportLevel = reportLevel;
    }

    public void Retire()
    {
        IsRetired = true;
    }

}