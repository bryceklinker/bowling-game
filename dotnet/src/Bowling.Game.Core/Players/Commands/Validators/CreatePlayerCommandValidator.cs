using FluentValidation;

namespace Bowling.Game.Core.Players.Commands.Validators;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().NotNull();
    }
}