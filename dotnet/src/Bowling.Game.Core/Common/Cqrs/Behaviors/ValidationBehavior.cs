using Bowling.Game.Core.Common.Cqrs.Commands;
using FluentValidation;
using MediatR;

namespace Bowling.Game.Core.Common.Cqrs.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
{
    private readonly IValidator<TRequest>[] _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators.ToArray();
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
            return await next().ConfigureAwait(false);

        if (_validators.Length > 1)
            throw new InvalidOperationException($"Found multiple validators for {typeof(TRequest).Name}");

        await _validators[0].ValidateAndThrowAsync(request, cancellationToken).ConfigureAwait(false);
        return await next().ConfigureAwait(false);
    }
}