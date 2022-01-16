namespace Aden.Domain.Entities;

public class Submission
{
    public int Id { get; set; }
    public DateOnly? DueDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public int? DataYear { get; set; }
    public Specification Specification { get; set; }
}