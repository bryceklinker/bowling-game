using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling.Game.Core.Tests.Support;

internal static class ServiceProviderFactory
{
    public static IServiceProvider Create()
    {
        return new ServiceCollection()
            .AddBowlingGameCore(opts => opts.UseInMemoryDatabase($"{Guid.NewGuid()}"))
            .AddLogging()
            .BuildServiceProvider();
    }
}