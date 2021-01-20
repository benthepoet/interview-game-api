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
        public async Task<IEnumerable<GameDTO>> GetGames(string q, string sort)
        {
            return await _service.ListGames(q, sort);
        }
    }
}
