using Aden.SharedKernal;

namespace Aden.Domain.Events;

public record SpecificationWasActivated(int Id, string FileName, string FileNumber): IDomainEvent;