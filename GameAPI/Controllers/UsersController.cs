using GameAPI.Models.DTOs;
using GameAPI.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.GetUser(id);
            return Ok(user);
        }

        [HttpPost("{userId}/games")]
        public async Task<IActionResult> PostUserGame(int userId, 
            [FromBody] PostGameRequest gameRequest)
        {
            await _service.AddGame(userId, gameRequest.GameId);

            return NoContent();
        }

        [HttpDelete("{userId}/games/{gameId}")]
        public IActionResult DeleteUserGame(int userId, int gameId)
        {
            _service.DeleteGame(userId, gameId);

            return NoContent();
        }

        [HttpPost("{userId}/comparison")]
        public IActionResult PostUserComparison(int userId, 
            [FromBody] PostUserComparisonRequest comparisonRequest)
        {
            UserComparisonDTO comparison;

            try
            {
                comparison = _service.GetComparison(userId, comparisonRequest.OtherUserId, comparisonRequest.Comparison);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(comparison);
        } 

        public class PostGameRequest
        {
            public int GameId { get; set; }
        }

        public class PostUserComparisonRequest
        {
            public int OtherUserId { get; set; }
            public string Comparison { get; set; }
        }
    }
}