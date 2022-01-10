namespace Aden.Domain.Entities;

public class Specification
{
    private Specification()
    {
    }

    public Specification(string fileNumber, string filename, ReportLevel reportLevel)
    {
        FileNumber = fileNumber;
        Filename = filename;
        ReportLevel = reportLevel;
    }

    public int Id { get; private set; }
    public string? FileNumber { get; private set; }
    public string? Filename { get; private set; }
    public ReportLevel? ReportLevel { get; private set; }

    public string? Section { get; private set; }
    public string? Application { get; private set; }
    public string? SupportGroup { get; private set; }
    public string? Collection { get; private set; }
    public string? SpecificationUrl { get; private set; }
    
    public string? FilenameFormat { get; private set; }
    public string? ReportAction { get; private set; }
    
    public bool? IsRetired { get; set; }  

    private readonly List<Submission> _submissions = new();
    public IEnumerable<Submission> Submissions => _submissions.AsReadOnly();
    
    public void Update(string fileNumber, string fileName, ReportLevel reportLevel)
    {
        Filename = fileName;
        FileNumber = fileNumber;
        ReportLevel = reportLevel;
    }

    public void Update(string fileNumber, string fileName, ReportLevel reportLevel, string application, 
        string supportGroup, string collection, string specificationUrl, string filenameFormat, string reportAction)
    {
        Update(fileNumber, fileName, reportLevel);
        Filename = fileName;
        FileNumber = fileNumber;
        ReportLevel = reportLevel;
        Application = application;
        SupportGroup = supportGroup;
        Collection = collection;
        SpecificationUrl = specificationUrl;
        FilenameFormat = filenameFormat;
        ReportAction = reportAction; 
    }
    public void Retire()
    {
        IsRetired = true;
        //TODO: Cancel existing submissions and child work items
    }

    public void Activate()
    {
        IsRetired = false; 
        //TODO: What to do when activating? 
    }
}