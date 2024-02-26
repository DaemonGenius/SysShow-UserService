using UserService.Core.CoreDomain;
using UserService.Core.User.Domain;

namespace UserService.Core.User.Events;

public class UserCreatedEvent : DomainEvent
{
    public Guid AuthId { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    
}
