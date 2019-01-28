using System.Linq;
using TradingCardGame.Domain;
using Xunit;


namespace TradingCardGame.Tests
{
    public class MatchEndTests
    {
        [Fact]
        public void DefaultMatchIsNotOver()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var expectedStatus = false;

            // Act
            var match = new Match(playerOne, playerTwo);

            // Assert
            Assert.Equal(expectedStatus, match.IsOver);
        }

        [Fact]
        public void MatchNotOverAfterPlayerUseACard()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.ActivePlayer.Hand.Insert(0, 1);
            var expectedStatus = false;

            // Act
            match.PlayCard(0);

            // Assert
            Assert.Equal(expectedStatus, match.IsOver);
        }

        [Fact]
        public void MatchOverAfterPlayerLooseAllHealth()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.Players.First(p => p != match.ActivePlayer).Health = 1;
            match.ActivePlayer.Hand.Insert(0, 1);
            var expectedStatus = true;

            // Act
            match.PlayCard(0);

            // Assert
            Assert.Equal(expectedStatus, match.IsOver);
        }
    }
}
