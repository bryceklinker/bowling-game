using Bowling.Game.Core.Game.Exceptions;
using Bowling.Game.Core.Players.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bowling.Game.Core.Game.Entities;

public class BowlingGameEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    
    public ICollection<PlayerBowlingGameEntity> Players { get; set; } = new List<PlayerBowlingGameEntity>();

    public PlayerBowlingGameEntity AddPlayer(PlayerEntity player)
    {
        var playerGame = new PlayerBowlingGameEntity
        {
            Game = this,
            Player = player
        };
        Players.Add(playerGame);
        return playerGame;
    }

    public void Roll(PlayerEntity player, int pins)
    {
        StartTime ??= DateTimeOffset.UtcNow;

        var playerGame = FindPlayerGame(player);
        playerGame.Roll(pins);
    }

    public int Score(PlayerEntity player)
    {
        var playerGame = FindPlayerGame(player);
        return playerGame.Score();
    }
    
    private PlayerBowlingGameEntity FindPlayerGame(PlayerEntity player)
    {
        var bowlingGamePlayer = Players.FirstOrDefault(p => p.Player.Id == player.Id);
        if (bowlingGamePlayer == null)
            throw new PlayerIsMissingFromGameException(this, player);
        return bowlingGamePlayer;
    }
}

public class BowlingGameEntityConfiguration : IEntityTypeConfiguration<BowlingGameEntity>
{
    public void Configure(EntityTypeBuilder<BowlingGameEntity> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        builder.Property(g => g.StartTime);
    }
}