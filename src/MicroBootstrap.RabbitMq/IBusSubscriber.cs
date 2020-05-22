using System;
using MicroBootstrap.Commands;
using MicroBootstrap.Events;
using MicroBootstrap.Messages;
using MicroBootstrap.Types;

namespace MicroBootstrap.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, CustomException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, CustomException, IRejectedEvent> onError = null) 
            where TEvent : IEvent;
    }
}
