using System;
using Common.Messages;
using Newtonsoft.Json;
namespace Game.Services.Messaging.Core.Messages.Events
{
    [MessageNamespace("game-event-sources")]
    public class GameEventSourceAdded : IEvent
    {
        public Guid Id { get; }
        public int Score { get; }
        public bool IsWin { get; }

        [JsonConstructor]
        public GameEventSourceAdded(Guid id, int score,bool isWin)
        {
            Id = id;
            Score = score;
            IsWin = isWin;
        }
    }
}