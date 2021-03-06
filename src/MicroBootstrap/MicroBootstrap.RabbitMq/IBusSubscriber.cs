﻿using System;
using MicroBootstrap.Commands;
using MicroBootstrap.Events;
using MicroBootstrap.Messages;
using MicroBootstrap.Types;

namespace MicroBootstrap.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(Func<TCommand, CustomException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(Func<TEvent, CustomException, IRejectedEvent> onError = null) 
            where TEvent : IEvent;
    }
}
