using MediatR;

namespace Bowling.Game.Core.Common.Cqrs.Commands;

public interface ICommand<out T> : IRequest<T>
{
    
}

public interface ICommand : ICommand<Unit>, IRequest
{
    
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    
}

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand
{
    
}