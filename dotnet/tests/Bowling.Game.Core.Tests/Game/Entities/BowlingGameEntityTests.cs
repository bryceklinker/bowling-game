using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Game.Exceptions;
using Bowling.Game.Core.Players.Entities;

namespace Bowling.Game.Core.Tests.Game.Entities;

public class BowlingGameEntityTests
{
    [Fact]
    public void WhenPlayerAddedToGameThenGameHasAnotherPlayer()
    {
        var game = new BowlingGameEntity();
        
        game.AddPlayer(new PlayerEntity());

        game.Players.Should().HaveCount(1);
    }

    [Fact]
    public void WhenPlayerRollsAllGutterBallsThenScoreForPlayerInGameIsZero()
    {
        var game = new BowlingGameEntity{Id = Guid.NewGuid()};
        var player = new PlayerEntity{Id = Guid.NewGuid()};
        game.AddPlayer(player);

        for (var i = 0; i < 20; i++) 
            game.Roll(player, 0);

        game.Score(player).Should().Be(0);
    }

    [Fact]
    public void WhenPlayerIsNotPartOfGameThenThrowsOnRoll()
    {
        var game = new BowlingGameEntity();
        
        var act = () => game.Roll(new PlayerEntity(), 10);

        act.Should().Throw<PlayerIsMissingFromGameException>();
    }

    [Fact]
    public void WhenFirstRollIsMadeThenStartTimeIsNow()
    {
        var game = new BowlingGameEntity();
        var player = new PlayerEntity();
        game.AddPlayer(player);

        game.Roll(player, 2);

        game.StartTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(50));
    }

    [Fact]
    public void WhenSecondRollIsMadeThenStartTimeIsUnchanged()
    {
        var game = new BowlingGameEntity();
        var player = new PlayerEntity();
        game.AddPlayer(player);

        game.Roll(player, 2);
        var expectedStartTime = game.StartTime;
        game.Roll(player, 3);

        game.StartTime.Should().Be(expectedStartTime);
    }
}