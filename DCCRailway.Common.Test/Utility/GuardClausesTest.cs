using NUnit.Framework;
using System;

namespace DCCRailway.Common.Utilities.Tests
{
    [TestFixture]
    public class GuardClausesTest
    {
        [Test]
        public void IsNotNull_ShouldNotThrowException_WhenArgumentIsNotNull()
        {
            // Arrange
            object argumentValue = new object();
            string argumentName = "argument";

            // Act & Assert
            Assert.DoesNotThrow(() => GuardClauses.IsNotNull(argumentValue, argumentName));
        }

        [Test]
        public void IsNotNull_ShouldThrowArgumentNullException_WhenArgumentIsNull()
        {
            // Arrange
            object argumentValue = null;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => GuardClauses.IsNotNull(argumentValue, argumentName));
        }

        [Test]
        public void IsNotNullOrEmpty_ShouldNotThrowException_WhenArgumentIsNotNullOrEmpty()
        {
            // Arrange
            string argumentValue = "value";
            string argumentName = "argument";

            // Act & Assert
            Assert.DoesNotThrow(() => GuardClauses.IsNotNullOrEmpty(argumentValue, argumentName));
        }

        [Test]
        public void IsNotNullOrEmpty_ShouldThrowArgumentNullException_WhenArgumentIsNull()
        {
            // Arrange
            string argumentValue = null;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => GuardClauses.IsNotNullOrEmpty(argumentValue, argumentName));
        }

        [Test]
        public void IsNotNullOrEmpty_ShouldThrowArgumentNullException_WhenArgumentIsEmpty()
        {
            // Arrange
            string argumentValue = string.Empty;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => GuardClauses.IsNotNullOrEmpty(argumentValue, argumentName));
        }

        [Test]
        public void IsNotZero_ShouldNotThrowException_WhenArgumentIsNotZero()
        {
            // Arrange
            int argumentValue = 10;
            string argumentName = "argument";

            // Act & Assert
            Assert.DoesNotThrow(() => GuardClauses.IsNotZero(argumentValue, argumentName));
        }

        [Test]
        public void IsNotZero_ShouldThrowArgumentException_WhenArgumentIsZero()
        {
            // Arrange
            int argumentValue = 0;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GuardClauses.IsNotZero(argumentValue, argumentName));
        }

        [Test]
        public void IsLessThan_ShouldNotThrowException_WhenArgumentIsLessThanMaxValue()
        {
            // Arrange
            int maxValue = 100;
            int argumentValue = 50;
            string argumentName = "argument";

            // Act & Assert
            Assert.DoesNotThrow(() => GuardClauses.IsLessThan(maxValue, argumentValue, argumentName));
        }

        [Test]
        public void IsLessThan_ShouldThrowArgumentException_WhenArgumentIsEqualToMaxValue()
        {
            // Arrange
            int maxValue = 100;
            int argumentValue = 100;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GuardClauses.IsLessThan(maxValue, argumentValue, argumentName));
        }

        [Test]
        public void IsLessThan_ShouldThrowArgumentException_WhenArgumentIsGreaterThanMaxValue()
        {
            // Arrange
            int maxValue = 100;
            int argumentValue = 150;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GuardClauses.IsLessThan(maxValue, argumentValue, argumentName));
        }

        [Test]
        public void IsMoreThan_ShouldNotThrowException_WhenArgumentIsMoreThanMinValue()
        {
            // Arrange
            int minValue = 0;
            int argumentValue = 10;
            string argumentName = "argument";

            // Act & Assert
            Assert.DoesNotThrow(() => GuardClauses.IsMoreThan(minValue, argumentValue, argumentName));
        }

        [Test]
        public void IsMoreThan_ShouldThrowArgumentException_WhenArgumentIsEqualToMinValue()
        {
            // Arrange
            int minValue = 0;
            int argumentValue = 0;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GuardClauses.IsMoreThan(minValue, argumentValue, argumentName));
        }

        [Test]
        public void IsMoreThan_ShouldThrowArgumentException_WhenArgumentIsLessThanMinValue()
        {
            // Arrange
            int minValue = 0;
            int argumentValue = -10;
            string argumentName = "argument";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GuardClauses.IsMoreThan(minValue, argumentValue, argumentName));
        }
    }
}