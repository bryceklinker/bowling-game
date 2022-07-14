using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Players.Entities;

namespace Bowling.Game.Core.Game.Exceptions;

public class PlayerIsMissingFromGameException : Exception
{
    public PlayerIsMissingFromGameException(BowlingGameEntity game, PlayerEntity player)
        : base($"Player {player.Name} ({player.Id}) is missing from game {game.Id}")
    {
        
    }
}