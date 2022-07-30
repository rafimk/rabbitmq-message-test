using RabbitMQ.Client;

namespace Message.Shared.Connections;

public interface IChannelFactory
{
    IModel Create();
}