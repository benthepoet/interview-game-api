using System.Collections.Generic;

namespace GameAPI.Services.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public IEnumerable<GameDTO> Games { get; set; }
    }
}