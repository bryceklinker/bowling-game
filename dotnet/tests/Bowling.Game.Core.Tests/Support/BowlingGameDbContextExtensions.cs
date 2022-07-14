using Bogus;
using Bowling.Game.Core.Common.Storage;
using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Players.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling.Game.Core.Tests.Support;

public static class BowlingGameDbContextExtensions
{
    private static readonly Faker Faker = new();
    
    public static BowlingGameEntity AddBowlingGame(this BowlingGameDbContext context)
    {
        var game = new BowlingGameEntity();
        context.AddAndSave(game);
        return game;
    }

    public static PlayerEntity AddPlayer(this BowlingGameDbContext context)
    {
        var player = new PlayerEntity
        {
            Name = Faker.Name.FirstName()
        };
        context.AddAndSave(player);
        return player;
    }

    private static void AddAndSave<T>(this BowlingGameDbContext context, T entity)
        where T : class
    {
        context.Add(entity);
        context.SaveChanges();
    }
}