using Aden.SharedKernal;

namespace Aden.Domain.Entities;

public class Specification: DomainEntity
{
    private Specification()
    {
    }

    public Specification(string fileNumber, string filename, ReportLevel reportLevel)
    {
        FileNumber = fileNumber;
        Filename = filename;
        ReportLevel = reportLevel;
        IsRetired = false;
    }
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

    public void Rename(string fileNumber, string fileName)
    {
        Filename = fileName;
        FileNumber = fileNumber;
    }

    public void UpdateReportProcessDetail(ReportLevel reportLevel, string filenameFormat)
    {
        ReportLevel = reportLevel;
        FilenameFormat = filenameFormat; 
    }

    public void UpdateApplicationDetails(string application, string supportGroup, string collection,
        string specificationUrl)
    {
        Application = application;
        SupportGroup = supportGroup;
        Collection = collection;
        SpecificationUrl = specificationUrl;
    }

    public void Retire()
    {
        IsRetired = true;
        //TODO: Domain Event to cancel in progress work and notify
    }

    public void Activate()
    {
        IsRetired = false;
        //TODO: What to do when activating? 
    }

    public void AddSubmission(Submission submission)
    {
        _submissions.Add(submission);
        //TODO: Domain Event to notify
    }
}