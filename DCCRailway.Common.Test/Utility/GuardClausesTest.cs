using NUnit.Framework;
using System;

namespace DCCRailway.Common.Utilities.Tests;

[TestFixture]
public class GuardClausesTest {
    [Test]
    public void IsNotNull_ShouldNotThrowException_WhenArgumentIsNotNull() {
        // Arrange
        var argumentValue = new object();
        var argumentName  = "argument";

        // Act & Assert
        Assert.DoesNotThrow(() => GuardClauses.IsNotNull(argumentValue, argumentName));
    }

    [Test]
    public void IsNotNull_ShouldThrowArgumentNullException_WhenArgumentIsNull() {
        // Arrange
        object argumentValue = null;
        var    argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => GuardClauses.IsNotNull(argumentValue, argumentName));
    }

    [Test]
    public void IsNotNullOrEmpty_ShouldNotThrowException_WhenArgumentIsNotNullOrEmpty() {
        // Arrange
        var argumentValue = "value";
        var argumentName  = "argument";

        // Act & Assert
        Assert.DoesNotThrow(() => GuardClauses.IsNotNullOrEmpty(argumentValue, argumentName));
    }

    [Test]
    public void IsNotNullOrEmpty_ShouldThrowArgumentNullException_WhenArgumentIsNull() {
        // Arrange
        string argumentValue = null;
        var    argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => GuardClauses.IsNotNullOrEmpty(argumentValue, argumentName));
    }

    [Test]
    public void IsNotNullOrEmpty_ShouldThrowArgumentNullException_WhenArgumentIsEmpty() {
        // Arrange
        var argumentValue = string.Empty;
        var argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => GuardClauses.IsNotNullOrEmpty(argumentValue, argumentName));
    }

    [Test]
    public void IsNotZero_ShouldNotThrowException_WhenArgumentIsNotZero() {
        // Arrange
        var argumentValue = 10;
        var argumentName  = "argument";

        // Act & Assert
        Assert.DoesNotThrow(() => GuardClauses.IsNotZero(argumentValue, argumentName));
    }

    [Test]
    public void IsNotZero_ShouldThrowArgumentException_WhenArgumentIsZero() {
        // Arrange
        var argumentValue = 0;
        var argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => GuardClauses.IsNotZero(argumentValue, argumentName));
    }

    [Test]
    public void IsLessThan_ShouldNotThrowException_WhenArgumentIsLessThanMaxValue() {
        // Arrange
        var maxValue      = 100;
        var argumentValue = 50;
        var argumentName  = "argument";

        // Act & Assert
        Assert.DoesNotThrow(() => GuardClauses.IsLessThan(maxValue, argumentValue, argumentName));
    }

    [Test]
    public void IsLessThan_ShouldThrowArgumentException_WhenArgumentIsEqualToMaxValue() {
        // Arrange
        var maxValue      = 100;
        var argumentValue = 100;
        var argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => GuardClauses.IsLessThan(maxValue, argumentValue, argumentName));
    }

    [Test]
    public void IsLessThan_ShouldThrowArgumentException_WhenArgumentIsGreaterThanMaxValue() {
        // Arrange
        var maxValue      = 100;
        var argumentValue = 150;
        var argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => GuardClauses.IsLessThan(maxValue, argumentValue, argumentName));
    }

    [Test]
    public void IsMoreThan_ShouldNotThrowException_WhenArgumentIsMoreThanMinValue() {
        // Arrange
        var minValue      = 0;
        var argumentValue = 10;
        var argumentName  = "argument";

        // Act & Assert
        Assert.DoesNotThrow(() => GuardClauses.IsMoreThan(minValue, argumentValue, argumentName));
    }

    [Test]
    public void IsMoreThan_ShouldThrowArgumentException_WhenArgumentIsEqualToMinValue() {
        // Arrange
        var minValue      = 0;
        var argumentValue = 0;
        var argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => GuardClauses.IsMoreThan(minValue, argumentValue, argumentName));
    }

    [Test]
    public void IsMoreThan_ShouldThrowArgumentException_WhenArgumentIsLessThanMinValue() {
        // Arrange
        var minValue      = 0;
        var argumentValue = -10;
        var argumentName  = "argument";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => GuardClauses.IsMoreThan(minValue, argumentValue, argumentName));
    }
}