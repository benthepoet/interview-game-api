using System.Threading.Tasks;

namespace GameAPI.Data.RAWG
{
    public interface IRAWGClient
    {
        Task<GameResponse> GetGame(int gameId);
        Task<GamesResponse> ListGames(string search, string sort);
    }
}
