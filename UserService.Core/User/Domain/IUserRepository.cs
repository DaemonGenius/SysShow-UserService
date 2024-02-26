using UserService.Core.CoreDomain;

namespace UserService.Core.User.Domain;

public interface IUserRepository : IRepository<UserIdentity, UserState>
{
}