using System.Threading.Tasks;
using Common.Handlers;
using Game.Services.EventProcessor.Core.DTO;
using Game.Services.EventProcessor.Core.Messages.Queries;
using Game.Services.EventProcessor.Core.Repositories;
namespace Game.Services.EventProcessor.Application.Handlers.Queries
{
    public class GetGameEventHandler : IQueryHandler<GetGameEventSource, GameEventSourceDto>
    {
        private readonly IGameEventSourceRepository _gameEventSourceRepository;

        public GetGameEventHandler(IGameEventSourceRepository gameEventSourceRepository)
        {
            _gameEventSourceRepository = gameEventSourceRepository;
        }

        public async Task<GameEventSourceDto> HandleAsync(GetGameEventSource query)
        {
            var gameEventSource = await _gameEventSourceRepository.GetAsync(query.Id);

            return gameEventSource == null ? null : new GameEventSourceDto
            {
                Id = gameEventSource.Id,
                IsWin = gameEventSource.IsWin,
                Score = gameEventSource.Score
            };
        }
    }
}