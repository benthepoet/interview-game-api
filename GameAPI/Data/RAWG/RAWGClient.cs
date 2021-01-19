using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Data.RAWG
{
    public class RAWGClient : IRAWGClient
    {
        public GameRecord GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameRecord> ListGames(string q, string sort)
        {
            throw new NotImplementedException();
        }
    }
}
