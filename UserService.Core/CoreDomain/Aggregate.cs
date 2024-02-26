namespace UserService.Core.CoreDomain;

public abstract class Aggregate<TIdentity, TState>
    where TIdentity : Identity
    where TState : State<TIdentity>
{
    private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
    protected TState State { get; private set; }

    public IReadOnlyList<IDomainEvent> Changes => _changes;
    public int Version { get; private set; }
    public TIdentity Identity => State.Identity;

    protected Aggregate(TState state)
    {
        State = state ?? throw new ArgumentNullException(nameof(state));
        Version = 0; // Initialize version.
    }



    // Placeholder for a method to publish events, to be implemented as needed.
    protected virtual void Publish(IDomainEvent @event) { /* Integration with event bus or messaging system */ }
}