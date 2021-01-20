using GameAPI.Data;
using GameAPI.Models;
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

        public GameDTO ConvertToGameDTO(Game game)
        {
            return new GameDTO
            {
                GameId = game.Id,
                Name = game.Name,
                Added = game.Added,
                Metacritic = game.Metacritic,
                Rating = game.Rating,
                Released = game.Released,
                Updated = game.Updated
            };
        }

        public async Task<GameDTO> GetGame(int gameId)
        {
            var game = await _client.GetGame(gameId);
            return ConvertToGameDTO(game);
        }

        public async Task<GameDTO[]> ListGames(string search, string sort)
        {
            var gameList = await _client.ListGames(search, sort);

            return gameList.Results
                .Select(game => ConvertToGameDTO(game))
                .ToArray();
        }
    }
}
