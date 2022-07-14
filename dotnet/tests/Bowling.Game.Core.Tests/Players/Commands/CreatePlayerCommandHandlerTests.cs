using Bowling.Game.Core.Common.Cqrs;
using Bowling.Game.Core.Common.Storage;
using Bowling.Game.Core.Players.Commands;
using Bowling.Game.Core.Players.Entities;
using Bowling.Game.Core.Tests.Support;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling.Game.Core.Tests.Players.Commands;

public class CreatePlayerCommandHandlerTests
{
    private readonly BowlingGameDbContext _context;
    private readonly IMessageBus _bus;

    public CreatePlayerCommandHandlerTests()
    {
        var provider = ServiceProviderFactory.Create();

        _context = provider.GetRequiredService<BowlingGameDbContext>();
        _bus = provider.GetRequiredService<IMessageBus>();
    }

    [Fact]
    public async Task WhenPlayerCreatedThenAddsPlayerToDatabase()
    {
        var command = new CreatePlayerCommand("Bill");
        await _bus.ExecuteAsync(command);

        _context.Set<PlayerEntity>().Should().HaveCount(1);
    }

    [Fact]
    public async Task WhenNameIsMissingFromCommandThenThrowsException()
    {
        var command = new CreatePlayerCommand("");
        var act = () => _bus.ExecuteAsync(command);

        await act.Should().ThrowAsync<ValidationException>();
    }
}