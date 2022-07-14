using Bowling.Game.Core.Common.Cqrs.Commands;
using Bowling.Game.Core.Common.Cqrs.Queries;
using Microsoft.Extensions.Logging;

namespace Bowling.Game.Core.Common.Cqrs.Logging;

public class LoggingMessageBus : IMessageBus
{
    private readonly IMessageBus _inner;
    private readonly ILogger<LoggingMessageBus> _logger;

    public LoggingMessageBus(IMessageBus inner, ILogger<LoggingMessageBus> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken token = default)
    {
        using (BeginScope(command))
            return await _inner.ExecuteAsync(command, token).ConfigureAwait(false);
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query, CancellationToken token = default)
    {
        using (BeginScope(query))
            return await _inner.ExecuteAsync(query, token).ConfigureAwait(false);
    }

    private IDisposable BeginScope(object queryOrCommand)
    {
        var type = queryOrCommand.GetType().Name;
        var kind = type.Contains("Command") ? "Command" : "Query";
        return _logger.BeginScope(new Dictionary<string, object>
        {
            { kind, type }
        });
    }
}