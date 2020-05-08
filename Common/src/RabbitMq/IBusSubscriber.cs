using System;
using Common.Messages;
using Common.Types;

namespace Common.RabbitMq
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
