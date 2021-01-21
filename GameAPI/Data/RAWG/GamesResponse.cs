using System.Collections.Generic;

namespace GameAPI.Data.RAWG
{
    public class GamesResponse
    {
        public IEnumerable<GameResponse> Results { get; set; }
    }
}
