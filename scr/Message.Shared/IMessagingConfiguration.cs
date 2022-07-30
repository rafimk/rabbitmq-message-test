using Microsoft.Extensions.DependencyInjection;

namespace Message.Shared;

public interface IMessagingConfiguration
{
    IServiceCollection Services { get; }
}

internal sealed record MessagingConfiguration(IServiceCollection Services) : IMessagingConfiguration;