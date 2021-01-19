using GameAPI.Services.DTOs;
using System;

namespace GameAPI.Services
{
    public class UserService : IUserService
    {
        public void AddGame(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public UserDTO CreateUser()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
