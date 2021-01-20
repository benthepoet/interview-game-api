using System.Collections.Generic;

namespace GameAPI.Models
{
    public class GamesResponse
    {
        public IEnumerable<GameResponse> Results { get; set; }
    }
}
