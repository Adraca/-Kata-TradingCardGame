using TradingCardGame.Domain;
using Xunit;

namespace TradingCardGame.Tests
{
    public class MatchTurnTurnTests
    {
        [Fact]
        public void OnEndTurnActivePlayerChange()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            var activeName = match.ActivePlayer.Name;

            // Act
            match.EndTurn();

            // Assert
            Assert.NotEqual(activeName, match.ActivePlayer.Name);
        }

        [Fact]
        public void OnEndTurnNewActivePlayerGainAManaTotal()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            var expectedTotal = Rules.DefaultMana + Rules.ManaGainPerTurn;

            // Act
            match.EndTurn();

            // Assert
            Assert.Equal(expectedTotal, match.ActivePlayer.Mana.Total);
        }

        [Fact]
        public void OnEndTurnNewActivePlayerManaIsRefilled()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);

            // Act
            match.EndTurn();

            // Assert
            Assert.Equal(match.ActivePlayer.Mana.Slots, match.ActivePlayer.Mana.Total);
        }

        [Fact]
        public void OnEndTurnNewActivePlayerCantHaveMoreThanMaximumManaSlots()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            var expectedMana = Rules.MaximumMana;

            // Act
            match.Players.ForEach(p => p.Mana.Gain(9));
            match.EndTurn();

            // Assert
            Assert.Equal(expectedMana, match.ActivePlayer.Mana.Slots);
        }

        [Fact]
        public void OnEndTurnNewActivePlayerCantHaveMoreThanMaximumManaTotal()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            var expectedMana = Rules.MaximumMana;

            // Act
            match.Players.ForEach(p => p.Mana.Gain(Rules.MaximumMana - Rules.ManaGainPerTurn));
            match.EndTurn();

            // Assert
            Assert.Equal(expectedMana, match.ActivePlayer.Mana.Total);
        }

        [Fact]
        public void OnEndTurnNewActivePlayerDrawACard()
        {
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            var expectedNumbeOfCards = Rules.DefaultHandSize + 2;

            // Act
            match.EndTurn();

            // Assert
            Assert.Equal(expectedNumbeOfCards, match.ActivePlayer.Hand.Count);
        }
    }
}
