using System.Linq;
using TradingCardGame.Domain;
using Xunit;


namespace TradingCardGame.Tests
{
    public class MatchInitTests
    {
        [Fact]
        public void MatchHasTwoPlayers()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            int expectedNumberOfPlayers = 2;

            // Act
            var newMatch = new Match(playerOne, playerTwo);

            // Assert
            Assert.Equal(expectedNumberOfPlayers, newMatch.Players.Count);
        }

        [Fact]
        public void MatchContainsFirstAddedPlayer()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");

            // Act
            var newMatch = new Match(playerOne, playerTwo);

            // Assert
            Assert.Contains(newMatch.Players, p => p == playerOne);
        }

        [Fact]
        public void MatchContainsSecondlyAddedPlayer()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");

            // Act
            var newMatch = new Match(playerOne, playerTwo);

            // Assert
            Assert.Contains(newMatch.Players, p => p == playerTwo);
        }

        [Fact]
        public void MatchHasAnActivePlayer()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");

            // Act
            var newMatch = new Match(playerOne, playerTwo);

            // Assert
            Assert.True(newMatch.ActivePlayer == playerOne || newMatch.ActivePlayer == playerTwo);
        }

        [Fact]
        public void MatchStartWithADefaultHandSizeForActivePlayer()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            int expectedHandSize = Rules.DefaultHandSize;

            // Act
            var match = new Match(playerOne, playerTwo);

            // Assert
            Assert.Equal(expectedHandSize, match.ActivePlayer.Hand.Count);
        }

        [Fact]
        public void MatchStartWithADefaultHandSizeForInactivePlayer()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            int expectedHandSize = Rules.DefaultHandSize + 1;

            // Act
            var match = new Match(playerOne, playerTwo);

            // Assert
            Assert.True(match.Players.Where(p => p != match.ActivePlayer).All(p => p.Hand.Count == expectedHandSize));
        }

        [Fact]
        public void NoDuplicateCardsDrawnWithDedupDeck()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");

            // Act
            var match = new Match(playerOne, playerTwo);

            // Assert
            Assert.True(match.Players[0].Hand.Distinct().Any());
            Assert.True(match.Players[1].Hand.Distinct().Any());
        }

        [Fact]
        public void OnMatchStartActivePlayerHasDefaultManaSlot()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            int expectedMana = Rules.DefaultMana;

            // Act
            var match = new Match(playerOne, playerTwo);

            // Assert
            Assert.Equal(expectedMana, match.ActivePlayer.Mana.Slots);
        }

        [Fact]
        public void OnMatchStartActivePlayerHasDefaultManaTotal()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            int expectedTotal = Rules.DefaultMana;

            // Act
            var match = new Match(playerOne, playerTwo);

            // Assert
            Assert.Equal(expectedTotal, match.ActivePlayer.Mana.Total);
        }
    }
}
