namespace UserService.Core.CoreDomain.Application;

using Dapr.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

public abstract class BaseService<TIdentity, TAggregate, TState, TRepository>
    where TIdentity : Identity
    where TAggregate : Aggregate<TIdentity, TState>, new()
    where TState : State<TIdentity>
    where TRepository : IRepository<TIdentity, TState>
{
    protected readonly TRepository Repository;
    private readonly DaprClient _daprClient;
    private readonly string _pubSubName;

    protected BaseService(TRepository repository, IConfiguration configuration, DaprClient daprClient)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        _pubSubName = configuration["Dapr:PubSubName"] ?? throw new InvalidOperationException("Dapr PubSubName is not configured.");
    }

    protected async Task<TIdentity> AddAsync(Func<TAggregate, Task> action, string topic, object metadata = null)
    {
        var state = Repository.Initialize();
        var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), state)!;
        
        await action(aggregate);
        await Repository.Save(state);
        
        await PublishAsync(topic, new DaprEvent { Identity = aggregate.Identity.Id, State = state, Metadata = metadata });
        return aggregate.Identity;
    }

    protected async Task UpdateAsync(TIdentity identity, Func<TAggregate, Task> action, string topic, object metadata = null)
    {
        var state = await Repository.FetchOrDefault(identity);
        if (state is null) throw new AggregateNotFoundException(typeof(TAggregate).Name);

        var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), state)!;
        await action(aggregate);
        
        await Repository.Save(state);
        await PublishAsync(topic, new DaprEvent { Identity = aggregate.Identity.Id, State = state, Metadata = metadata });
    }

    private async Task PublishAsync<TEvent>(string topic, TEvent @event)
    {
        await _daprClient.PublishEventAsync(_pubSubName, topic, @event);
    }
}

public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(string message) : base(message) { }
}
