using System;
using System.Threading.Tasks;
using Game.Services.Messaging.Core.Services;
using Microsoft.AspNetCore.SignalR;

namespace Game.Services.Messaging.Application.Services
{
    public class HubWrapper : IHubWrapper
    {
        private readonly IHubContext<GameHub> _hubContext;

        public HubWrapper(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task PublishToUserAsync(Guid userId, string message, object data)
            => await _hubContext.Clients.Group(userId.ToUserGroup()).SendAsync(message, data);

        public async Task PublishToAllAsync(string message, object data)
            => await _hubContext.Clients.All.SendAsync(message, data);
    }
}