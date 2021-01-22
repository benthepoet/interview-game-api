using GameAPI.Data;
using GameAPI.Exceptions;
using GameAPI.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IGameService _gameService;

        private const string COMPARISON_INTERSECTION = "intersection";
        private const string COMPARISON_DIFFERENCE = "difference";
        private const string COMPARISON_UNION = "union";

        public UserService(IUserRepository repository, IGameService gameService)
        {
            _repository = repository;
            _gameService = gameService;
        }

        public async Task AddGame(int userId, int gameId)
        {
            var user = _repository.GetUser(userId);

            if (user == null)
            {
                throw new EntityNotFoundException($"User {userId} does not exist.");
            }

            var game = await _gameService.GetGame(gameId);

            if (game == null)
            {
                throw new ArgumentException($"Game {gameId} does not exist.");
            }

            if (user.GameIds.Contains(gameId))
            {
                throw new DuplicateEntityException($"Game {gameId} is already in the user's list.");
            }

            user.GameIds.Add(gameId);
            _repository.UpdateUser(user);
        }

        public UserDTO CreateUser()
        {
            var user = _repository.CreateUser();

            return new UserDTO
            {
                UserId = user.Id,
                Games = user.GameIds
                    .Select(x => new GameDTO
                    {
                        GameId = x
                    })
                    .ToArray()
            };
        }

        public void DeleteGame(int userId, int gameId)
        {
            var user = _repository.GetUser(userId);

            if (user == null)
            {
                throw new EntityNotFoundException($"User {userId} does not exist.");
            }

            if (!user.GameIds.Contains(gameId))
            {
                throw new EntityNotFoundException($"Game {gameId} is not in the user's list.");
            }

            user.GameIds.Remove(gameId);
            _repository.UpdateUser(user);
        }

        public async Task<UserComparisonDTO> GetComparison(int userId, int otherUserId, string comparison)
        {
            var user = _repository.GetUser(userId);

            if (user == null)
            {
                throw new EntityNotFoundException($"User {userId} does not exist.");
            }

            var otherUser = _repository.GetUser(otherUserId);

            if (otherUser == null)
            {
                throw new ArgumentException($"Other user {otherUserId} does not exist.");
            }

            // Create a new hash set for output as the methods below mutate the data structure
            HashSet<int> gameIds;

            switch (comparison)
            {
                case COMPARISON_DIFFERENCE:
                    gameIds = new HashSet<int>(otherUser.GameIds);
                    gameIds.ExceptWith(user.GameIds);
                    break;

                case COMPARISON_INTERSECTION:
                    gameIds = new HashSet<int>(user.GameIds);
                    gameIds.IntersectWith(otherUser.GameIds);
                    break;

                case COMPARISON_UNION:
                    gameIds = new HashSet<int>(user.GameIds);
                    gameIds.UnionWith(otherUser.GameIds);
                    break;

                default:
                    throw new ArgumentException($"'{comparison}' is not a valid comparison.");
            }

            var games = await ConvertGameIdsToGameDTOs(gameIds);

            return new UserComparisonDTO
            {
                UserId = user.Id,
                OtherUserId = otherUser.Id,
                Comparison = comparison,
                Games = games
            };
        }

        public async Task<UserDTO> GetUser(int userId)
        {
            var user = _repository.GetUser(userId);
            
            if (user == null)
            {
                throw new EntityNotFoundException($"User {userId} does not exist.");
            }

            var games = await ConvertGameIdsToGameDTOs(user.GameIds);

            return new UserDTO
            {
                UserId = user.Id,
                Games = games
            };
        }

        private async Task<IEnumerable<GameDTO>> ConvertGameIdsToGameDTOs(HashSet<int> gameIds)
        {
            var games = new List<GameDTO>();
            
            foreach (var gameId in gameIds)
            {
                games.Add(await _gameService.GetGame(gameId));
            }

            return games;
        }
    }
}
