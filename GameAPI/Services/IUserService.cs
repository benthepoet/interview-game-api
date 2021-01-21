using GameAPI.Services.DTOs;
using System.Threading.Tasks;

namespace GameAPI.Services
{
    public interface IUserService
    {
        UserDTO CreateUser();
        Task<UserDTO> GetUser(int userId);
        Task AddGame(int userId, int gameId);
        void DeleteGame(int userId, int gameId);
        Task<UserComparisonDTO> GetComparison(int userId, int otherUserId, string comparison);
    }
}