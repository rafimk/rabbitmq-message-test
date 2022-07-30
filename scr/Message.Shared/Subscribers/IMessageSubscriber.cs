﻿using RabbitMQ.Client.Events;

namespace Message.Shared.Subscribers;

public interface IMessageSubscriber
{
    IMessageSubscriber SubscribeMessage<TMessage>(string queue, string routingKey, string exchange,
        Func<TMessage, BasicDeliverEventArgs, Task> handle) where TMessage : class, IMessage;
}