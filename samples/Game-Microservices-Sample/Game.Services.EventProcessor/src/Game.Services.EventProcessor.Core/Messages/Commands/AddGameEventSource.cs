using System;
using Newtonsoft.Json;
using MicroBootstrap.Commands;

namespace Game.Services.EventProcessor.Core.Messages.Commands
{
    public class AddGameEventSource : ICommand
    {
        public Guid Id { get; }
        public int Score { get; }
        public bool IsWin { get; }

        [JsonConstructor]
        public AddGameEventSource(Guid id, int score, bool isWin)
        {
            Id = id;
            Score = score;
            IsWin = isWin;
        }
    }
}