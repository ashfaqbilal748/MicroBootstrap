using Microsoft.AspNetCore.Mvc;

namespace Game.Services.EventProcessor.API.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Game Event Processor Service");
    }
}