using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MicroBootstrap.Authentication;

namespace Game.Services.Messaging.Application.Services
{
    public class GameHub : Hub
    {
        public GameHub()
        {
        }
        public async Task InitializeAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisconnectAsync();
            }
            try
            {
                await ConnectAsync();
            }
            catch
            {
                await DisconnectAsync();
            }
        }

        private async Task ConnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("connected");
        }

        private async Task DisconnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
        }
    }
}