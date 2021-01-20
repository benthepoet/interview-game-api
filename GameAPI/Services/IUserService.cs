using GameAPI.Models.DTOs;
using System.Threading.Tasks;

namespace GameAPI.Services
{
    public interface IUserService
    {
        UserDTO CreateUser();
        Task<UserDTO> GetUser(int userId);
        Task AddGame(int userId, int gameId);
        void DeleteGame(int userId, int gameId);
        UserComparisonDTO GetComparison(int userId, int otherUserId, string comparison);
    }
}