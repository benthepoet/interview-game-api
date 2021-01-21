using GameAPI.Data.RAWG;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameAPI.Tests
{
    [TestClass]
    public class RAWGClientTests
    {
        [DataTestMethod]
        [DataRow("title")]
        [DataRow("-title")]
        public void IsSortValid_InvalidFields_ReturnFalse(string sort)
        {
            var result = RAWGClient.IsSortValid(sort);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("name")]
        [DataRow("released")]
        [DataRow("added")]
        [DataRow("created")]
        [DataRow("updated")]
        [DataRow("rating")]
        [DataRow("metacritic")]
        public void IsSortValid_ValidAscendingFields_ReturnTrue(string sort)
        {
            var result = RAWGClient.IsSortValid(sort);

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("-name")]
        [DataRow("-released")]
        [DataRow("-added")]
        [DataRow("-created")]
        [DataRow("-updated")]
        [DataRow("-rating")]
        [DataRow("-metacritic")]
        public void IsSortValid_ValidDescendingFields_ReturnTrue(string sort)
        {
            var result = RAWGClient.IsSortValid(sort);

            Assert.IsTrue(result);
        }
    }
}
