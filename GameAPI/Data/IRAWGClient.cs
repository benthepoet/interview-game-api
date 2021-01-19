using GameAPI.Models;
using System.Collections.Generic;

namespace GameAPI.Data
{
    interface IRAWGClient
    {
        Game GetGame(int id);
        IEnumerable<Game> ListGames(string search, string sort);
    }
}
