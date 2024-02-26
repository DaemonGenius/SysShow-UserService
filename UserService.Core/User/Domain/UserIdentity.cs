using UserService.Core.CoreDomain;

namespace UserService.Core.User.Domain;


public class UserIdentity : Identity
{
    public UserIdentity(Guid id) : base(id) { }
}
