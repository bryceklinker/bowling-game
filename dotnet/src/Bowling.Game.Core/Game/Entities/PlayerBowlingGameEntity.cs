using Bowling.Game.Core.Players.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bowling.Game.Core.Game.Entities;

public class PlayerBowlingGameEntity
{
    public Guid Id { get; set; }
    public int CurrentRoll { get; set; } = 0;

    public BowlingGameEntity Game { get; set; }
    public PlayerEntity Player { get; set; }

    public ICollection<PlayerBowlingGameRollEntity> Rolls { get; set; }

    public PlayerBowlingGameEntity()
    {
        CurrentRoll = 0;
        Rolls = Enumerable.Range(0, 21)
            .Select(_ => new PlayerBowlingGameRollEntity { Pins = 0, PlayerBowlingGame = this })
            .ToList();
    }

    public void Roll(int pins)
    {
        var roll = Rolls.ElementAt(CurrentRoll);
        roll.Pins = pins;
        CurrentRoll++;
    }

    public int Score()
    {
        var rolls = Rolls.Select(r => r.Pins).ToArray();
        var roll = 0;
        var score = 0;
        for (var frame = 0; frame < 10; frame++)
        {
            if (rolls[roll] == 10)
            {
                score += 10 + rolls[roll + 1] + rolls[roll + 2];
                roll++;
            }
            else if (rolls[roll] + rolls[roll + 1] == 10)
            {
                score += 10 + rolls[roll + 2];
                roll += 2;
            }
            else
            {
                score += rolls[roll] + rolls[roll + 1];
                roll += 2;
            }
            
        }
        return score;
    }
}

public class PlayerBowlingGameEntityConfiguration : IEntityTypeConfiguration<PlayerBowlingGameEntity>
{
    public void Configure(EntityTypeBuilder<PlayerBowlingGameEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.CurrentRoll).IsRequired();

        builder.HasOne(p => p.Game)
            .WithMany(g => g.Players)
            .IsRequired();

        builder.HasOne(p => p.Player)
            .WithMany(g => g.Games)
            .IsRequired();
    }
}