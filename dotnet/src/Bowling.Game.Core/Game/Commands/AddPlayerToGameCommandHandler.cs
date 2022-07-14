using Bowling.Game.Core.Common.Cqrs.Commands;
using Bowling.Game.Core.Common.Exceptions;
using Bowling.Game.Core.Common.Storage;
using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Players.Entities;
using MediatR;

namespace Bowling.Game.Core.Game.Commands;

public record AddPlayerToGameCommand(Guid GameId, Guid PlayerId) : ICommand;

public class AddPlayerToGameCommandHandler : AsyncRequestHandler<AddPlayerToGameCommand>, ICommandHandler<AddPlayerToGameCommand>
{
    private readonly BowlingGameDbContext _context;

    public AddPlayerToGameCommandHandler(BowlingGameDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(AddPlayerToGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.FindAsync<BowlingGameEntity>(request.GameId).ConfigureAwait(false);
        if (game == null)
            throw new EntityNotFoundException<BowlingGameEntity>(request.GameId);
        
        var player = await _context.FindAsync<PlayerEntity>(request.PlayerId).ConfigureAwait(false);
        if (player == null)
            throw new EntityNotFoundException<PlayerEntity>(request.PlayerId);
        
        game.AddPlayer(player);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}