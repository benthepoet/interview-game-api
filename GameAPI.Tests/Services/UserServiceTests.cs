using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Models.DTOs;
using GameAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        public UserService userService;

        [TestInitialize]
        public void Setup()
        {
            var user1 = new User
            {
                Id = 1,
                GameIds = new HashSet<int> { 16, 32, 48, 64 }
            };

            var user2 = new User
            {
                Id = 2,
                GameIds = new HashSet<int> { 32, 48, 72, 96 }
            };

            var userRepository = new Mock<IUserRepository>();
            
            userRepository
                .Setup(x => x.GetUser(1))
                .Returns(user1);

            userRepository
                .Setup(x => x.GetUser(2))
                .Returns(user2);
            
            var gameService = new Mock<IGameService>();

            foreach (var gameId in user1.GameIds) {
                gameService
                    .Setup(x => x.GetGame(gameId).Result)
                    .Returns(new GameDTO
                    {
                        GameId = gameId
                    });
            }

            foreach (var gameId in user2.GameIds)
            {
                gameService
                    .Setup(x => x.GetGame(gameId).Result)
                    .Returns(new GameDTO
                    {
                        GameId = gameId
                    });
            }

            userService = new UserService(userRepository.Object, gameService.Object);
        }

        [TestMethod]
        public async Task GetComparison_Union_ReturnsBoth()
        {
            var comparison = await userService.GetComparison(1, 2, "union");
            Assert.IsNotNull(comparison.Games.FirstOrDefault(x => x.GameId == 48));
        }
    }
}
