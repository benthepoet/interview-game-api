using GameAPI.Data;
using GameAPI.Models;
using System;
using System.Linq;

namespace GameAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void AddGame(int userId, int gameId)
        {
            var user = _repository.GetUser(userId);

            if (user != null)
            {
                user.GameIds.Add(gameId);
                _repository.UpdateUser(user);
            }
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
            throw new NotImplementedException();
        }

        public UserComparisonDTO GetComparison(int userId, int otherUserId, string comparison)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUser(int userId)
        {
            var user = _repository.GetUser(userId);
            
            if (user == null)
            {
                return null;
            }

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
    }
}
