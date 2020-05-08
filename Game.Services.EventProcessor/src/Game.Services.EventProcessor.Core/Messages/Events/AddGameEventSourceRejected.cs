using System;
using Common.Messages;
using Newtonsoft.Json;

namespace Game.Services.EventProcessor.Core.Messages.Events
{
    public class AddGameEventSourceRejected : IRejectedEvent
    {
        public Guid GameEventSourceId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public AddGameEventSourceRejected(Guid gameEventSourceId, string reason, string code)
        {
            GameEventSourceId = gameEventSourceId;
            Reason = reason;
            Code = code;
        }
    }
}