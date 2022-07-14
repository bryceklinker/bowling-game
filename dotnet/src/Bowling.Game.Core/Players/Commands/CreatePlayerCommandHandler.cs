using Bowling.Game.Core.Common.Cqrs.Commands;
using Bowling.Game.Core.Common.Storage;
using Bowling.Game.Core.Players.Entities;
using Bowling.Game.Core.Players.Models;

namespace Bowling.Game.Core.Players.Commands;

public record CreatePlayerCommand(string Name) : ICommand<PlayerModel>;

public class CreatePlayerCommandHandler : ICommandHandler<CreatePlayerCommand, PlayerModel>
{
    private readonly BowlingGameDbContext _context;

    public CreatePlayerCommandHandler(BowlingGameDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerModel> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new PlayerEntity
        {
            Name = request.Name
        };
        await _context.AddAsync(player, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return player.ToModel();
    }
}