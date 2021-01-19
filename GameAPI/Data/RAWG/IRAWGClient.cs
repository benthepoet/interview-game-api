using System.Collections.Generic;

namespace GameAPI.Data.RAWG
{
    interface IRAWGClient
    {
        GameRecord GetGame(int id);
        IEnumerable<GameRecord> ListGames(string q, string sort);
    }
}
