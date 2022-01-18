using Aden.SharedKernal;

namespace Aden.Domain.Entities;

public class Submission: DomainEntity
{
    private Submission()
    {
        
    }

    public Submission(DateOnly dueDate, int dataYear)
    {
        DueDate = dueDate;
        DataYear = dataYear;
        SubmissionState = SubmissionState.NotStarted; 
    }
    
    public DateOnly? DueDate { get; private set; }
    public DateTime? SubmissionDate { get; private set; }
    public int DataYear { get; private set; }

    public SubmissionState SubmissionState { get; private set; }
    public Specification Specification { get; private set; }
}

public enum SubmissionState: byte
{
    NotStarted = 1, 
    AssignedForGeneration = 2    
}