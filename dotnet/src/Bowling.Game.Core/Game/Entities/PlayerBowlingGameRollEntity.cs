using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bowling.Game.Core.Game.Entities;

public class PlayerBowlingGameRollEntity
{
    public Guid Id { get; set; }
    public int Pins { get; set; } = 0;
    public PlayerBowlingGameEntity PlayerBowlingGame { get; set; }
}

public class PlayerBowlingGameRollEntityConfiguration : IEntityTypeConfiguration<PlayerBowlingGameRollEntity>
{
    public void Configure(EntityTypeBuilder<PlayerBowlingGameRollEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Pins).IsRequired();

        builder.HasOne(p => p.PlayerBowlingGame)
            .WithMany(g => g.Rolls)
            .IsRequired();
    }
}