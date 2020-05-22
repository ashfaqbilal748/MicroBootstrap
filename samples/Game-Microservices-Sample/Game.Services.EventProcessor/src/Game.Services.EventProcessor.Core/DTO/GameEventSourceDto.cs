using System;

namespace Game.Services.EventProcessor.Core.DTO
{
    public class GameEventSourceDto
    {
        public Guid Id { get; set; }
        public bool IsWin { get; set; }
        public int Score { get; set; }
    }
}