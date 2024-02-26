namespace UserService.Core.CoreDomain;


public abstract class DomainEvent: IDomainEvent
{
    public DateTimeOffset OccurredOn { get; protected set; } = DateTimeOffset.UtcNow;
}
