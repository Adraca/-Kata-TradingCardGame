using System.Collections.Generic;
using TradingCardGame.Domain;
using Xunit;

namespace TradingCardGame.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void DefaultHealthIsThirty()
        {
            // Arrange
            int expectedLife = Rules.DefaultHealth;

            // Act
            var newPlayer = new Player("MyPlayer");

            //Assert
            Assert.Equal(expectedLife, newPlayer.Health);
        }

        [Fact]
        public void DefaultManaIsOneSlot()
        {
            // Arrange
            int expectedMana = Rules.DefaultMana;

            // Act
            var newPlayer = new Player("MyPlayer");

            //Assert
            Assert.Equal(expectedMana, newPlayer.Mana.Slots);
        }

        [Fact]
        public void DefaultTotalManaIsOne()
        {
            // Arrange
            int expectedTotal = Rules.DefaultMana;

            // Act
            var newPlayer = new Player("MyPlayer");

            //Assert
            Assert.Equal(expectedTotal, newPlayer.Mana.Total);
        }

        [Fact]
        public void PlayerHasAName()
        {
            // Arrange
            string expectedName = "MyPlayer";

            // Act
            var newPlayer = new Player(expectedName);

            //Assert
            Assert.Equal(expectedName, newPlayer.Name);
        }

        [Fact]
        public void PlayerHasADeck()
        {
            // Arrange
            var expectedDeck = new List<int>(4) { 0, 1, 2, 3, 4 };

            // Act
            var newPlayer = new Player("MyPlayer");

            //Assert
            Assert.Equal(expectedDeck.Count, newPlayer.InitialDeck.Count);
        }
    }
}
