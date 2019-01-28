using System.Collections.Generic;
using TradingCardGame.Domain;
using Xunit;

namespace TradingCardGame.Tests
{
    public class PlayerDrawTests
    {
        [Fact]
        public void PlayerDrawCardHasACardInHand()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedHandSize = 1;

            // Act
            player.DrawCard();

            // Assert
            Assert.Equal(expectedHandSize, player.Hand.Count);
        }

        [Fact]
        public void PlayerDrawCardHasOneLessCardInDeck()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedDeckSize = player.Deck.Count - 1;

            // Act
            player.DrawCard();

            // Assert
            Assert.Equal(expectedDeckSize, player.Deck.Count);
        }

        [Fact]
        public void PlayerDrawMultipleCardsInHand()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedHandSize = 2;

            // Act
            player.DrawCard();
            player.DrawCard();

            // Assert
            Assert.Equal(expectedHandSize, player.Hand.Count);
        }

        [Fact]
        public void PlayerDrawHasLessCardsInDeck()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedDeckSize = player.Deck.Count - 2;

            // Act
            player.DrawCard();
            player.DrawCard();

            // Assert
            Assert.Equal(expectedDeckSize, player.Deck.Count);
        }

        [Fact]
        public void PlayerOverloadHandSize()
        {
            // Arrange
            var player = new Player("MyPlayer");
            player.Deck.AddRange(new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 });
            var expectedHandSize = Rules.MaximumHandSize;

            // Act
            for (int i = 0; i < Rules.MaximumHandSize + 1; i++)
            {
                player.DrawCard();
            }

            // Assert
            Assert.Equal(expectedHandSize, player.Hand.Count);
        }

        [Fact]
        public void PlayerOverloadLostCard()
        {
            // Arrange
            var player = new Player("MyPlayer");
            player.Deck.AddRange(new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 });
            var expectedDeckSize = player.Deck.Count - (Rules.MaximumHandSize + 1);

            // Act
            for (int i = 0; i < Rules.MaximumHandSize + 1; i++)
            {
                player.DrawCard();
            }

            // Assert
            Assert.Equal(expectedDeckSize, player.Deck.Count);
        }

        [Fact]
        public void PlayerBleedOutDeckSizeIsZero()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedDeckSize = 0;
            var numberOfDraws = player.Deck.Count + 1;

            // Act
            for (int i = 0; i < numberOfDraws; i++)
            {
                player.DrawCard();
            }

            // Assert
            Assert.Equal(expectedDeckSize, player.Deck.Count);
        }

        [Fact]
        public void PlayerBleedOutLooseOneLife()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedHealth = Rules.DefaultHealth - 1;
            var numberOfDraws = player.Deck.Count + 1;

            // Act
            for (int i = 0; i < numberOfDraws; i++)
            {
                player.DrawCard();
            }

            // Assert
            Assert.Equal(expectedHealth, player.Health);
        }

        [Fact]
        public void PlayerTwoBleedOutLooseOnePlusTwoLife()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedHealth = Rules.DefaultHealth - 3; // -1, -2
            var numberOfDraws = player.Deck.Count + 2;

            // Act
            for (int i = 0; i < numberOfDraws; i++)
            {
                player.DrawCard();
            }

            // Assert
            Assert.Equal(expectedHealth, player.Health);
        }

        [Fact]
        public void PlayerTwoBleedOutLooseOneMoreLifeEachTurn()
        {
            // Arrange
            var player = new Player("MyPlayer");
            var expectedHealth = Rules.DefaultHealth - 15; // -1, -2, -3, -4, -5
            var numberOfDraws = player.Deck.Count + 5;

            // Act
            for (int i = 0; i < numberOfDraws; i++)
            {
                player.DrawCard();
            }

            // Assert
            Assert.Equal(expectedHealth, player.Health);
        }
    }
}
