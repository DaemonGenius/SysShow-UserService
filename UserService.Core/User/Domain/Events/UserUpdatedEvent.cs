using UserService.Core.CoreDomain;
using UserService.Core.User.Domain;

namespace UserService.Core.User.Events;

public class UserUpdatedEvent : DomainEvent
{
    public UserIdentity UserIdentity { get; }
    // Other properties

    public UserUpdatedEvent(UserIdentity userIdentity)
    {
        UserIdentity = userIdentity;
    }
}