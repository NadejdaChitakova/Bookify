using Bookify.Domain.Abstractions;

namespace Bookify.Domain.AttachedFiles.Events;

public sealed record UploadPhotoDomainEvent(Guid id) : IDomainEvent;