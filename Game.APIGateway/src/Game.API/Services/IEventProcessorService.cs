using System;
using System.Threading.Tasks;
using Common.Types;
using Game.API.DTO;
using Game.API.Messages.Queries;
using RestEase;

namespace Game.API.Services
{
    //API Gateway connect to internal RestEase and its config for load balancing and choose type of forwarding
    //request to internal services
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IEventProcessorService
    {
        [AllowAnyStatusCode]
        [Get("game-event-sources/{id}")]
        Task<GameEventSourceDto> GetAsync([Path] Guid id);

        [AllowAnyStatusCode]
        [Get("game-event-sources")]
        Task<PagedResult<GameEventSourceDto>> BrowseAsync([Query] BrowseGameEventSource query);
    }
}