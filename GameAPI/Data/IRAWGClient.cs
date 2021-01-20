using GameAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameAPI.Data
{
    public interface IRAWGClient
    {
        Task<GameResponse> GetGame(int gameId);
        Task<GamesResponse> ListGames(string search, string sort);
    }
}
