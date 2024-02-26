using UserService.Core.CoreDomain;
using UserService.Core.User.Events;

namespace UserService.Core.User.Domain;

public class UserAggregate : Aggregate<UserIdentity, UserState>
{

    public UserAggregate() : base(null)
    {
    }

    public UserAggregate(UserState state) : base(state)
    {
    }

    public void UserCreated(
        Guid authId,
        string username,
        string firstName,
        string lastName,
        Gender gender,
        int age
    )
    {
        var @event = new UserCreatedEvent
        {
            AuthId = authId,
            Username = username,
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            Age = age,
        };
        
        State.Apply(@event);
        
    }   
    
    public void UserActivated(
    )
    {
        var @event = new UserActivatedEvent() { };
        
        State.Apply(@event);
    }
    
    public void UserDisabled(
    )
    {
        var @event = new UserDisabledEvent() { };
        
        State.Apply(@event);
    }


}