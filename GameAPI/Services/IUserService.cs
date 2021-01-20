using GameAPI.Models.DTOs;

namespace GameAPI.Services
{
    public interface IUserService
    {
        UserDTO CreateUser();
        UserDTO GetUser(int userId);
        void AddGame(int userId, int gameId);
        void DeleteGame(int userId, int gameId);
        UserComparisonDTO GetComparison(int userId, int otherUserId, string comparison);
    }
}