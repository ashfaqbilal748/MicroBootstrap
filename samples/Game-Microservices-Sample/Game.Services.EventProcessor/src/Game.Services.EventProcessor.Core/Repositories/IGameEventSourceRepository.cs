using System;
using System.Linq;
using System.Threading.Tasks;
using Game.Services.EventProcessor.Core.Entities;
using Game.Services.EventProcessor.Core.Messages.Queries;
using MicroBootstrap.Queries;

namespace Game.Services.EventProcessor.Core.Repositories
{
    public interface IGameEventSourceRepository
    {
        Task AddAsync(GameEventSource gameEventSource);
        Task UpdateAsync(GameEventSource gameEventSource);
        Task<GameEventSource> GetAsync(Guid id);
        IQueryable<GameEventSource> GetAll();
        Task<PagedResult<GameEventSource>> BrowseAsync(BrowseGameEventSource query);
    }
}