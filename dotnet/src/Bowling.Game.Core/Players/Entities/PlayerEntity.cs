using Bowling.Game.Core.Game.Entities;
using Bowling.Game.Core.Players.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bowling.Game.Core.Players.Entities;

public class PlayerEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<PlayerBowlingGameEntity> Games { get; set; } = new List<PlayerBowlingGameEntity>();

    public PlayerModel ToModel()
    {
        return new PlayerModel(Id, Name);
    }
}

public class PlayerEntityConfiguration : IEntityTypeConfiguration<PlayerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired();
    }
}