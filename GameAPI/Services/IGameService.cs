using GameAPI.Models.DTOs;
using System.Threading.Tasks;

namespace GameAPI.Services
{
    public interface IGameService
    {
        GameDTO GetGame(int gameId);
        Task<GameDTO[]> ListGames(string search, string sort);
    }
}