using System;

namespace Game.Services.Messaging.Application.Services
{
    public static class Extensions
    {
        public static string ToUserGroup(this Guid userId) 
            => $"users:{userId}";
    }
}