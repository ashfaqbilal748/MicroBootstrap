using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Types;
using Game.Services.EventProcessor.Core.Entities;
using Game.Services.EventProcessor.Core.Messages.Queries;

namespace Game.Services.EventProcessor.Core.Repositories
{
    public interface IGameEventSourceRepository
    {
        Task AddAsync(GameEventSource discount);
        Task UpdateAsync(GameEventSource discount);
        Task<GameEventSource> GetAsync(Guid id);
        IQueryable<GameEventSource> GetAll();
        Task<PagedResult<GameEventSource>> BrowseAsync(BrowseGameEventSource query);
    }
}