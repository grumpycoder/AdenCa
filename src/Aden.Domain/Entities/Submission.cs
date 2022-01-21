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

    public void Start()
    {
        //TODO: What happens when starting Submission? 
        SubmissionState = SubmissionState.AssignedForGeneration;
    }
}

public enum SubmissionState: byte
{
    NotStarted = 1,
    AssignedForGeneration = 2,
    AssignedForReview = 3,
    AwaitingApproval = 4,
    AssignedForSubmission = 5,
    CompleteWithError = 6,
    Complete = 7,
    Waived = 8,  
}