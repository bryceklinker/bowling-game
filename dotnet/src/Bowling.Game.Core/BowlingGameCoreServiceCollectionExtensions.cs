using System.Reflection;
using Bowling.Game.Core.Common.Cqrs;
using Bowling.Game.Core.Common.Cqrs.Logging;
using Bowling.Game.Core.Common.Storage;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Bowling.Game.Core;

public static class BowlingGameCoreServiceCollectionExtensions
{
    private static readonly Assembly CoreAssembly = typeof(BowlingGameCoreServiceCollectionExtensions).Assembly;
    
    public static IServiceCollection AddBowlingGameCore(this IServiceCollection services, Action<DbContextOptionsBuilder> configureDbContext)
    {
        services.AddMediatR(CoreAssembly)
            .AddFluentValidation(new[] { CoreAssembly })
            .AddAutoMapper(CoreAssembly);
        services.AddDbContext<BowlingGameDbContext>(configureDbContext);
        services.TryAddTransient<IMessageBus>(p =>
        {
            var mediator = p.GetRequiredService<IMediator>();
            var logger = p.GetRequiredService<ILogger<LoggingMessageBus>>();
            return new LoggingMessageBus(new MessageBus(mediator), logger);
        });
        services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}