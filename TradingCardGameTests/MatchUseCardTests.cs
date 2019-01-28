using System;
using System.Linq;
using TradingCardGame.Domain;
using Xunit;

namespace TradingCardGame.Tests
{
    public class MatchUseCardTests
    {
        [Fact]
        public void PlayerUseCardHandSizeReduced()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.ActivePlayer.Hand.Insert(0, 1);

            var expectedHandSize = match.ActivePlayer.Hand.Count - 1;

            // Act
            match.PlayCard(0);

            // Assert
            Assert.Equal(expectedHandSize, match.ActivePlayer.Hand.Count);
        }

        [Fact]
        public void PlayerUseCardLooseMana()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.ActivePlayer.Hand.Insert(0, 1);

            int expectedMana = match.ActivePlayer.Mana.Slots - 1;

            // Act
            match.PlayCard(0);

            // Assert
            Assert.Equal(expectedMana, match.ActivePlayer.Mana.Slots);
        }

        [Fact]
        public void PlayerUseNullManaCardDoesNotLooseMana()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.ActivePlayer.Hand.Insert(0, 0);

            int expectedMana = match.ActivePlayer.Mana.Slots;

            // Act
            match.PlayCard(0);

            // Assert
            Assert.Equal(expectedMana, match.ActivePlayer.Mana.Slots);
        }

        [Fact]
        public void PlayerCannotUseTooExpensiveCard()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.ActivePlayer.Hand.Insert(0, 10);

            // Act
            Action act = () => match.PlayCard(0);

            // Assert
            Assert.Throws<NotEnoughManaException>(act);
        }

        [Fact]
        public void PlayerCanUseMultipleCards()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.ActivePlayer.Hand.Insert(0, 0);
            match.ActivePlayer.Hand.Insert(0, 0);
            match.ActivePlayer.Hand.Insert(0, 0);

            int expectedHand = match.ActivePlayer.Hand.Count - 3;

            // Act
            match.PlayCard(0);
            match.PlayCard(0);
            match.PlayCard(0);

            // Assert
            Assert.Equal(expectedHand, match.ActivePlayer.Hand.Count);
        }

        [Fact]
        public void AttackedPlayerLooseHealth()
        {
            // Arrange
            var playerOne = new Player("PlayerOne");
            var playerTwo = new Player("PlayerTwo");
            var match = new Match(playerOne, playerTwo);
            match.ActivePlayer.Hand.Insert(0, 1);

            int expectedHealth = Rules.DefaultHealth - 1;

            // Act
            match.PlayCard(0);

            // Assert
            Assert.Equal(expectedHealth, match.Players.First(p => p != match.ActivePlayer).Health);
        }
    }
}
