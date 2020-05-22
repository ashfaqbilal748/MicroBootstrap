using System;
using MicroBootstrap.Queries;
using Game.API.DTO;

namespace Game.API.Messages.Queries
{
    public class GetGameEventSource : IQuery<GameEventSourceDto>
    {
        public Guid Id { get; set; } 
    }
}