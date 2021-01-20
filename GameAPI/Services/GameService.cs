using GameAPI.Data;
using GameAPI.Models.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IRAWGClient _client;

        public GameService(IRAWGClient client)
        {
            _client = client;
        }

        public GameDTO GetGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<GameDTO[]> ListGames(string search, string sort)
        {
            var gameList = await _client.ListGames(search, sort);

            return gameList.Results
                .Select(x => new GameDTO
                {
                    GameId = x.Id,
                    Name = x.Name
                })
                .ToArray();
        }
    }
}
