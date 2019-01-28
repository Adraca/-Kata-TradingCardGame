using System;
using TradingCardGame.Domain;
using Xunit;

namespace TradingCardGame.Tests
{
    public class ManaTests
    {
        [Fact]
        public void DefaultManaIsOne()
        {
            // Arrange
            int expectedMana = Rules.DefaultMana;

            // Act
            var mana = new Mana();

            // Assert
            Assert.Equal(expectedMana, mana.Slots);
        }

        [Fact]
        public void DefaultTotalManaIsOne()
        {
            // Arrange
            int expectedMana = Rules.DefaultMana;

            // Act
            var mana = new Mana();

            // Assert
            Assert.Equal(expectedMana, mana.Total);
        }

        [Fact]
        public void UsingManaEmptyTheSlots()
        {
            // Arrange
            int expectedMana = 0;
            var mana = new Mana();

            // Act
            mana.Use(1);

            // Assert
            Assert.Equal(expectedMana, mana.Slots);
        }

        [Fact]
        public void UsingManaDontChangeTotal()
        {
            // Arrange
            int expectedTotal = Rules.DefaultMana;
            var mana = new Mana();

            // Act
            mana.Use(1);

            // Assert
            Assert.Equal(expectedTotal, mana.Total);
        }

        [Fact]
        public void CantUseMoreThanCurrentManaSlots()
        {
            // Arrange
            var mana = new Mana();

            // Act
            Action act = () => mana.Use(10);

            // Assert
            Assert.Throws<NotEnoughManaException>(act);
        }

        [Fact]
        public void GainingManaAddSlots()
        {
            // Arrange
            int expectedMana = 2;
            var mana = new Mana();

            // Act
            mana.Gain(1);

            // Assert
            Assert.Equal(expectedMana, mana.Slots);
        }

        [Fact]
        public void GainingManaAddToTotal()
        {
            // Arrange
            int expectedTotal = 2;
            var mana = new Mana();

            // Act
            mana.Gain(1);

            // Assert
            Assert.Equal(expectedTotal, mana.Total);
        }

        [Fact]
        public void CantGainMoreThanMaximumMana()
        {
            // Arrange
            var mana = new Mana();

            // Act
            Action act = () => mana.Gain(Rules.MaximumMana);

            // Assert
            Assert.Throws<TooMuchManaException>(act);
        }

        [Fact]
        public void TemporaryGainManaAddSlots()
        {
            // Arrange
            int expectedMana = 2;
            var mana = new Mana();

            // Act
            mana.TemporalGain(1);

            // Assert
            Assert.Equal(expectedMana, mana.Slots);
        }

        [Fact]
        public void TemporaryGainManaDontAddToTotal()
        {
            // Arrange
            int expectedTotal = Rules.DefaultMana;
            var mana = new Mana();

            // Act
            mana.TemporalGain(1);

            // Assert
            Assert.Equal(expectedTotal, mana.Total);
        }

        [Fact]
        public void GainTotalManaDontAddSlots()
        {
            // Arrange
            int expectedSlots = Rules.DefaultMana;
            var mana = new Mana();

            // Act
            mana.GainTotal(1);

            // Assert
            Assert.Equal(expectedSlots, mana.Slots);
        }

        [Fact]
        public void GainTotalManaAddToTotal()
        {
            // Arrange
            int expectedTotal = Rules.DefaultMana + 1;
            var mana = new Mana();

            // Act
            mana.GainTotal(1);

            // Assert
            Assert.Equal(expectedTotal, mana.Total);
        }

        [Fact]
        public void ManaSlotsEqualsTotalAfterRefill()
        {
            // Arrange
            var mana = new Mana();

            // Act
            mana.Refill();

            // Assert
            Assert.True(mana.Slots == mana.Total);
        }

        [Fact]
        public void ManaSlotsEqualsTotalAfterGainAndRefill()
        {
            // Arrange
            int expectedSlots = Rules.DefaultMana + 2;
            var mana = new Mana();

            // Act
            mana.GainTotal(2);
            mana.Refill();

            // Assert
            Assert.Equal(expectedSlots, mana.Slots);
        }
    }
}
