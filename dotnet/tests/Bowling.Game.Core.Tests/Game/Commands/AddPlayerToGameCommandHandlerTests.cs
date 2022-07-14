using Bowling.Game.Core.Common.Cqrs;
using Bowling.Game.Core.Common.Exceptions;
using Bowling.Game.Core.Common.Storage;
using Bowling.Game.Core.Game.Commands;
using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Players.Entities;
using Bowling.Game.Core.Tests.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling.Game.Core.Tests.Game.Commands;

public class AddPlayerToGameCommandHandlerTests
{
    private readonly BowlingGameDbContext _context;
    private readonly BowlingGameEntity _game;
    private readonly PlayerEntity _player;
    private readonly IMessageBus _bus;

    public AddPlayerToGameCommandHandlerTests()
    {
        var provider = ServiceProviderFactory.Create();

        _context = provider.GetRequiredService<BowlingGameDbContext>();
        _game = _context.AddBowlingGame();
        _player = _context.AddPlayer();
        
        _bus = provider.GetRequiredService<IMessageBus>();
    }

    [Fact]
    public async Task WhenPlayerIsAddedToGameThenPlayerGameIsAddedToDatabase()
    {
        var command = new AddPlayerToGameCommand(_game.Id, _player.Id);

        await _bus.ExecuteAsync(command);

        _context.Set<PlayerBowlingGameEntity>().Should().HaveCount(1);
    }

    [Fact]
    public async Task WhenPlayerIsMissingThenThrowsException()
    {
        var command = new AddPlayerToGameCommand(_game.Id, Guid.NewGuid());

        var act = () => _bus.ExecuteAsync(command);

        await act.Should().ThrowAsync<EntityNotFoundException<PlayerEntity>>();
    }

    [Fact]
    public async Task WhenGameIsMissingThenThrowsException()
    {
        var command = new AddPlayerToGameCommand(Guid.NewGuid(), _player.Id);

        var act = () => _bus.ExecuteAsync(command);

        await act.Should().ThrowAsync<EntityNotFoundException<BowlingGameEntity>>();
    }
}