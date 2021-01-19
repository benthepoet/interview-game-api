using GameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Data
{
    public class RAWGClient : IRAWGClient
    {
        public Game GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> ListGames(string search, string sort)
        {
            throw new NotImplementedException();
        }
    }
}
