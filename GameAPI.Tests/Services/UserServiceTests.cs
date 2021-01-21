using GameAPI.Data;
using GameAPI.Services;
using GameAPI.Services.DTOs;
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
                GameIds = new HashSet<int> { 16, 32, 48 }
            };

            var user2 = new User
            {
                Id = 2,
                GameIds = new HashSet<int> { 32, 48, 72 }
            };

            var userRepository = new Mock<IUserRepository>();
            
            userRepository
                .Setup(x => x.GetUser(user1.Id))
                .Returns(user1);

            userRepository
                .Setup(x => x.GetUser(user2.Id))
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
        public async Task GetComparison_Difference_ReturnsUniqueOnOther()
        {
            var comparison = await userService.GetComparison(1, 2, "difference");
            var gameIds = comparison.Games.Select(x => x.GameId).ToArray();

            Assert.IsTrue(gameIds.Length == 1);
            Assert.IsNotNull(gameIds.Contains(72));
        }

        [TestMethod]
        public async Task GetComparison_Intersection_ReturnsBoth()
        {
            var comparison = await userService.GetComparison(1, 2, "intersection");
            var gameIds = comparison.Games.Select(x => x.GameId).ToArray();

            Assert.IsTrue(gameIds.Length == 2);
            Assert.IsNotNull(gameIds.Contains(32));
            Assert.IsNotNull(gameIds.Contains(48));
        }

        [TestMethod]
        public async Task GetComparison_Union_ReturnsBoth()
        {
            var comparison = await userService.GetComparison(1, 2, "union");
            var gameIds = comparison.Games.Select(x => x.GameId).ToArray();

            Assert.IsTrue(gameIds.Length == 4);
            Assert.IsNotNull(gameIds.Contains(16));
            Assert.IsNotNull(gameIds.Contains(32));
            Assert.IsNotNull(gameIds.Contains(48));
            Assert.IsNotNull(gameIds.Contains(72));
        }
    }
}
