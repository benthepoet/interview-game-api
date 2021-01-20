using GameAPI.Models.DTOs;
using System.Threading.Tasks;

namespace GameAPI.Services
{
    public interface IGameService
    {
        Task<GameDTO> GetGame(int gameId);
        Task<GameDTO[]> ListGames(string search, string sort);
    }
}