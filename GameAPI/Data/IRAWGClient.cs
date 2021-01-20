using GameAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameAPI.Data
{
    public interface IRAWGClient
    {
        Task<Game> GetGame(int gameId);
        Task<GameList> ListGames(string search, string sort);
    }
}
