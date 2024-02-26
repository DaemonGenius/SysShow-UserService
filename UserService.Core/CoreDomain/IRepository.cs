namespace UserService.Core.CoreDomain;

public interface IRepository<TIdentity, TState>
    where TIdentity : Identity
    where TState : State<TIdentity>
{
    Task<TState?> FetchOrDefault(TIdentity identity);

    TState Initialize();

    Task Save(TState state);
}