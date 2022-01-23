using Aden.Domain.Common;
using Aden.Domain.Events;

namespace Aden.Domain.Entities;

public class Specification : DomainEntity
{
    private Specification()
    {
    }

    public Specification(string fileNumber, string filename, ReportLevel reportLevel)
    {
        FileNumber = fileNumber;
        FileName = filename;
        ReportLevel = reportLevel;
        IsRetired = false;
    }

    public string? FileNumber { get; private set; }
    public string? FileName { get; private set; }
    public ReportLevel? ReportLevel { get; private set; }

    public string? Section { get; private set; }
    public string? Application { get; private set; }
    public string? SupportGroup { get; private set; }
    public string? Collection { get; private set; }
    public string? SpecificationUrl { get; private set; }

    public string? FilenameFormat { get; private set; }
    public string? ReportAction { get; private set; }

    public bool IsRetired { get; set; }

    private readonly List<Submission> _submissions = new();
    public IEnumerable<Submission> Submissions => _submissions.AsReadOnly();

    public void Rename(string fileNumber, string fileName)
    {
        FileName = fileName;
        FileNumber = fileNumber;
    }

    public void UpdateReportProcessDetail(ReportLevel reportLevel, string filenameFormat, string reportAction)
    {
        ReportLevel = reportLevel;
        FilenameFormat = filenameFormat;
        ReportAction = reportAction;
    }

    public void UpdateApplicationDetails(string application, string supportGroup, string collection,
        string section, string specificationUrl)
    {
        Application = application;
        SupportGroup = supportGroup;
        Collection = collection;
        Section = section;
        SpecificationUrl = specificationUrl;
    }

    public void Retire()
    {
        IsRetired = true;
        _submissions.RemoveAll(s => s.SubmissionState < SubmissionState.CompleteWithError);

        //REVIEW: Domain Event to cancel in progress work and notify
        AddDomainEvents(new SpecificationWasRetired(Id, FileName, FileNumber));
    }

    public void Activate()
    {
        IsRetired = false;
        //REVIEW: What to do when activating?
        AddDomainEvents(new SpecificationWasActivated(Id, FileName, FileNumber));
    }

    public void AddSubmission(Submission submission)
    {
        _submissions.Add(submission);
        //REVIEW: Domain Event to notify
    }
}