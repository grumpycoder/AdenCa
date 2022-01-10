using Aden.Domain.Entities;

namespace Aden.Domain;

public class Submission
{
    public int Id { get; set; }
    public DateOnly? DueDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public int? DataYear { get; set; }
    public Specification Specification { get; set; }
}