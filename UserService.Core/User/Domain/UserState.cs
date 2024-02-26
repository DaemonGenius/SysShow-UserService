using UserService.Core.CoreDomain;
using UserService.Core.User.Events;

namespace UserService.Core.User.Domain;

public class UserState: State<UserIdentity>
{
    public UserState(UserIdentity userIdentity): base(userIdentity)
    {
    }

    protected UserState()
    {
    }
    
    public Guid AuthId { get; private set; }
    public string Username { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender Gender { get; private set; }
    public int Age { get; private set; }
    public UserStatus Status { get; private set; }
    public Metadata Metadata { get; private set; }
    
    public void Apply(UserCreatedEvent @event)
    {
        AuthId = @event.AuthId;
        Username = @event.Username;
        FirstName = @event.FirstName;
        LastName = @event.LastName;
        Gender = @event.Gender;
        Age = @event.Age;
        
        Metadata =  new Metadata { CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
        Status = UserStatus.Created;
    }   
    
    public void Apply(UserActivatedEvent @event)
    {
        Metadata.UpdatedAt = DateTime.UtcNow;
        Status = UserStatus.Active;
    }
    
    public void Apply(UserDisabledEvent @event)
    {
        Metadata.UpdatedAt = DateTime.UtcNow;
        Status = UserStatus.Active;
    }
}

public enum UserStatus
{
    Created = 0,
    Active = 1,
    Disabled = 2
}

public enum Gender
{
    Male,
    Female,
    Other
}
