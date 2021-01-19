using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GameAPI.Services.DTOs;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public UserDTO PostUser()
        {
            return null;
        } 

        [HttpGet("{id}")]
        public UserDTO GetUser(int id)
        {
            return null;
        }

        [HttpPost("{id}/games")]
        public ActionResult PostGame(int id)
        {
            return null;
        }

        [HttpDelete("{id}/games/{gameId}")]
        public ActionResult DeleteGame(int id, int gameId)
        {
            return null;
        }

        [HttpPost("{id}/comparison")]
        public UserComparisonDTO PostUserComparison(int id)
        {
            return null;
        } 
    }
}