using System.Collections.Generic;

namespace GameAPI.Services.DTOs
{
    public class UserComparisonDTO
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public string Comparison { get; set; }
        public IEnumerable<GameDTO> Games { get; set; }
    }
}