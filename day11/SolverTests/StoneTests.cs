using AoC;

namespace SolverTests;

public class StoneTests
{
    [Test]
    public void TestBlink0()
    {
        // Arrange
        Stone stone = new(0);
        List<Stone> expected = [new(1)];

        // Act
        List<Stone> actual = stone.Blink();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestBlinkEvenNrDigitsAllZeroes()
    {
        // Arrange
        Stone stone = new(1000);
        List<Stone> expected = [new(10), new(0)];

        // Act
        List<Stone> actual = stone.Blink();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestBlinkEvenNrDigits()
    {
        // Arrange
        Stone stone = new(1001);
        List<Stone> expected = [new(10), new(1)];

        // Act
        List<Stone> actual = stone.Blink();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestBlinkUnevenNrDigits()
    {
        // Arrange
        Stone stone = new(10001);
        List<Stone> expected = [new(10001 * 2024)];

        // Act
        List<Stone> actual = stone.Blink();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}