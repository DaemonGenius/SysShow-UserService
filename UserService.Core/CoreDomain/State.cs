namespace UserService.Core.CoreDomain;

public abstract class State<TIdentity> where TIdentity : Identity
{
    public TIdentity Identity { get; }

    public State(TIdentity identity)
    {
        Identity = identity ?? throw new ArgumentNullException(nameof (identity));
    }

    protected State()
    {
    }
}