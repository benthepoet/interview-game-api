using GameAPI.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameAPI.Services
{
    public interface IGameService
    {
        Task<GameDTO> GetGame(int gameId);
        Task<IEnumerable<GameDTO>> ListGames(string search, string sort);
    }
}