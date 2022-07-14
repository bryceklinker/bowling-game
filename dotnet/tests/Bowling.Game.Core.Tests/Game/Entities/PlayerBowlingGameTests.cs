using Bowling.Game.Core.Game.Entities;

namespace Bowling.Game.Core.Tests.Game.Entities;

public class PlayerBowlingGameTests
{
    private readonly PlayerBowlingGameEntity _playerBowlingGame;

    public PlayerBowlingGameTests()
    {
        _playerBowlingGame = new PlayerBowlingGameEntity();
    }
    
    [Fact]
    public void WhenPlayerRollsAllGutterBallsThenScoreIsZero()
    {
        RollMany(20, 0);

        _playerBowlingGame.Score().Should().Be(0);
    }

    [Fact]
    public void WhenPlayerRollsAllOpenFramesThenScoreIsSumOfAllRolls()
    {
        RollMany(20, 3);

        _playerBowlingGame.Score().Should().Be(60);
    }

    [Fact]
    public void WhenPlayerRollsASpareThenScoreIsTenPlusNextRoll()
    {
        _playerBowlingGame.Roll(3);
        _playerBowlingGame.Roll(7);
        _playerBowlingGame.Roll(5);
        RollMany(17, 0);

        _playerBowlingGame.Score().Should().Be(20);
    }

    [Fact]
    public void WhenPlayerRollsAStrikeThenScoreIsTenPlusNextTwoRolls()
    {
        _playerBowlingGame.Roll(10);
        _playerBowlingGame.Roll(7);
        _playerBowlingGame.Roll(5);
        RollMany(16, 0);

        _playerBowlingGame.Score().Should().Be(34);
    }

    [Fact]
    public void WhenPlayerRollsAllStrikesThenScoreIsThreeHundred()
    {
        RollMany(12, 10);

        _playerBowlingGame.Score().Should().Be(300);
    }

    private void RollMany(int rolls, int pins)
    {
        for (var i = 0; i < rolls; i++) _playerBowlingGame.Roll(pins);
    }
}