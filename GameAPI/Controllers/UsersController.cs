using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult PostUser()
        {
            var user = _service.CreateUser();
            var uri = $"{Request.GetDisplayUrl()}/{user.UserId}";
            return Created(uri, user);
        } 

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _service.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("{id}/games")]
        public IActionResult PostGame(int id, [FromBody] PostGameRequest gameRequest)
        {
            var user = _service.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            _service.AddGame(id, gameRequest.GameId);

            return NoContent();
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

        public class PostGameRequest
        {
            public int GameId { get; set; }
        }
    }
}