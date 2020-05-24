using System;
using MicroBootstrap.Types;
namespace Game.Services.EventProcessor.Core.Entities
{
    public class GameEventSource : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public bool IsWin { get; set; }
        public int Score { get; set; }

        private GameEventSource()
        {
        }

        public GameEventSource(Guid id, bool isWin, int score)
        {
            if (id == Guid.Empty)
            {
                throw new CustomException("Invalid_GameEventSource_Id", "Invalid GameEventSource Id.");
            }

            if (score < 0)
            {
                throw new CustomException("Invalid_Score",
                    $"Invalid Score: {score}, The score can't be negative.");
            }

            Id = id;
            Score = score;
            IsWin = isWin;
        }

    }
}