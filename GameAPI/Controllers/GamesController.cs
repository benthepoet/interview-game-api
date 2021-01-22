using GameAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _service;

        public GamesController(IGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames([FromQuery] GetGamesRequest gamesRequest)
        {
            var games = await _service.ListGames(gamesRequest.q, gamesRequest.sort);
            return Ok(games);
        }

        public class GetGamesRequest 
        {
            [Required]
            public string q { get; set; }
            public string sort { get; set; }
        }
    }
}
