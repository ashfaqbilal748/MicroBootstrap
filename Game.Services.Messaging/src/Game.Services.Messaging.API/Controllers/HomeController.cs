using Microsoft.AspNetCore.Mvc;

namespace Game.Services.Messaging.API
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Game Messaging-SignalR Service");
    }
}