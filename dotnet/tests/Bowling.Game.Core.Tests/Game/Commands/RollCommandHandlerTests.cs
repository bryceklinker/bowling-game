using Bowling.Game.Core.Common.Cqrs;
using Bowling.Game.Core.Common.Exceptions;
using Bowling.Game.Core.Common.Storage;
using Bowling.Game.Core.Game.Commands;
using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Players.Entities;
using Bowling.Game.Core.Tests.Support;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling.Game.Core.Tests.Game.Commands;

public class RollCommandHandlerTests
{
    private readonly IMessageBus _bus;
    private readonly BowlingGameEntity _game;
    private readonly PlayerEntity _player;
    private readonly PlayerBowlingGameEntity _playerGame;

    public RollCommandHandlerTests()
    {
        var provider = ServiceProviderFactory.Create();

        var context = provider.GetRequiredService<BowlingGameDbContext>();
        _game = context.AddBowlingGame();
        _player = context.AddPlayer();
        _playerGame = _game.AddPlayer(_player);
        context.SaveChanges();
        
        _bus = provider.GetRequiredService<IMessageBus>();
    }

    [Fact]
    public async Task WhenPlayerRollsThenAddsRollToPlayerGame()
    {
        var command = new RollCommand(_game.Id, _player.Id, 5);
        await _bus.ExecuteAsync(command);

        _game.StartTime.Should().NotBeNull();
        _playerGame.Rolls.ElementAt(0).Pins.Should().Be(5);
    }

    [Fact]
    public async Task WhenPinsIsLessThanZeroThenThrowsException()
    {
        var command = new RollCommand(_game.Id, _player.Id, -1);

        var act = () => _bus.ExecuteAsync(command);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenPinsIsGreaterThanTenThenThrowsException()
    {
        var command = new RollCommand(_game.Id, _player.Id, 11);

        var act = () => _bus.ExecuteAsync(command);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenGameIsMissingThenThrowsException()
    {
        var command = new RollCommand(Guid.NewGuid(), _player.Id, 5);

        var act = () => _bus.ExecuteAsync(command);

        await act.Should().ThrowAsync<EntityNotFoundException<BowlingGameEntity>>();
    }

    [Fact]
    public async Task WhenPlayerIsMissingThenThrowsException()
    {
        var command = new RollCommand(_game.Id, Guid.NewGuid(), 4);

        var act = () => _bus.ExecuteAsync(command);

        await act.Should().ThrowAsync<EntityNotFoundException<PlayerEntity>>();
    }
}