using GameAPI.Models.DTOs;

namespace GameAPI.Services
{
    public interface IGameService
    {
        GameDTO GetGame(int gameId);
        GameDTO[] ListGames(string query, string sort);
    }
}