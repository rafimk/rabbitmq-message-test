﻿using Message.Shared.Accessors;
using Message.Shared.Connections;
using Message.Shared.Dispatchers;
using Message.Shared.Publishers;
using Message.Shared.Subscribers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Message.Shared;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, Action<IMessagingConfiguration> configure = default)
    {
        var factory = new ConnectionFactory {HostName = "localhost"};
        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<ChannelAccessor>();
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>();
        services.AddSingleton<IMessageIdAccessor, MessageIdAccessor>();

        services.Scan(cfg => cfg.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IMessageHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        configure?.Invoke(new MessagingConfiguration(services));
        
        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}