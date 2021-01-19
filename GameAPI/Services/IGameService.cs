using GameAPI.Models;

namespace GameAPI.Services
{
    public interface IGameService
    {
        GameDTO[] ListGames(string query, string sort);
    }
}