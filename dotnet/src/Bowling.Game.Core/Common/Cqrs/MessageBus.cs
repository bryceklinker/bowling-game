using Bowling.Game.Core.Common.Cqrs.Commands;
using Bowling.Game.Core.Common.Cqrs.Queries;
using MediatR;

namespace Bowling.Game.Core.Common.Cqrs;

public interface IMessageBus
{
    Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken token = default);
    Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query, CancellationToken token = default);
}

public class MessageBus : IMessageBus
{
    private readonly IMediator _mediator;

    public MessageBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken token = default)
    {
        return await _mediator.Send(command, token).ConfigureAwait(false);
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query, CancellationToken token = default)
    {
        return await _mediator.Send(query, token).ConfigureAwait(false);
    }
}