using Microsoft.EntityFrameworkCore;

namespace Bowling.Game.Core.Common.Storage;

public class BowlingGameDbContext : DbContext
{
    public BowlingGameDbContext(DbContextOptions<BowlingGameDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BowlingGameDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}