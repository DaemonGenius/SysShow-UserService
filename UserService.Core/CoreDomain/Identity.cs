namespace UserService.Core.CoreDomain;

public abstract class Identity
{
    public Guid Id { get; protected set; }

    protected Identity(Guid id)
    {
        Id = id;
    }
}