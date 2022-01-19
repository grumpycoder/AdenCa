using Aden.SharedKernal;

namespace Aden.Domain.Events;

public record SpecificationWasRetired(int SpecificationId, string FileName, string FileNumber) : IDomainEvent; 

// public class SpecificationWasRetired: IDomainEvent
// {
//     public int SpecificationId { get; set; }
//     public string FileName { get; set; }
//
//     public string FileNumber { get; set; }
// }