using Bowling.Game.Core.Common.Cqrs.Commands;
using Bowling.Game.Core.Common.Exceptions;
using Bowling.Game.Core.Common.Storage;
using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Players.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bowling.Game.Core.Game.Commands;

public record RollCommand(Guid GameId, Guid PlayerId, int Pins) : ICommand;

public class RollCommandHandler : AsyncRequestHandler<RollCommand>, ICommandHandler<RollCommand>
{
    private readonly BowlingGameDbContext _context;

    public RollCommandHandler(BowlingGameDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(RollCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.Set<BowlingGameEntity>()
            .Where(p => p.Id == request.GameId)
            .Include(p => p.Players)
            .ThenInclude(p => p.Rolls)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (game == null)
            throw new EntityNotFoundException<BowlingGameEntity>(request.GameId);

        var player = await _context.FindAsync<PlayerEntity>(request.PlayerId).ConfigureAwait(false);
        if (player == null)
            throw new EntityNotFoundException<PlayerEntity>(request.PlayerId);

        game.Roll(player, request.Pins);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}