using System.Collections.Generic;

namespace GameAPI.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public IEnumerable<GameDTO> Games { get; set; }
    }
}