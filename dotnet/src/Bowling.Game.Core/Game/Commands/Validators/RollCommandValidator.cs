using FluentValidation;

namespace Bowling.Game.Core.Game.Commands.Validators;

public class RollCommandValidator :  AbstractValidator<RollCommand>
{
    public RollCommandValidator()
    {
        RuleFor(r => r.Pins)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(10);
    }
}