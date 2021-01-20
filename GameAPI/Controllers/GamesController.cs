using GameAPI.Models.DTOs;
using GameAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetGames(string q, string sort)
        {
            if (string.IsNullOrEmpty(q))
            {
                return BadRequest("The 'q' parameter is required and cannot be empty.");
            }

            var games = await _service.ListGames(q, sort);
            return Ok(games);
        }
    }
}
