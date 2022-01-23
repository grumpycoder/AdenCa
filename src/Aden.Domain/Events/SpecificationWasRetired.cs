using Aden.Domain.Interfaces;

namespace Aden.Domain.Events;

public record SpecificationWasRetired(int SpecificationId, string FileName, string FileNumber) : IDomainEvent; 
