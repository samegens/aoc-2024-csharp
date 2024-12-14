using AoC;

namespace SolverTests;

public class RectTests
{
    [TestCase(2, 0, true)]
    [TestCase(0, 2, false)]
    public void TestContains(int x, int y, bool expected)
    {
        // Arrange
        Rect sut = new(new Point2d(0, 0), new Point2d(2, 1));

        // Act
        bool actual = sut.Contains(new Point2d(x, y));

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestWidth()
    {
        // Arrange
        Rect sut = new(new Point2d(1, 2), new Point2d(3, 5));

        // Act
        long actual = sut.Width;

        // Assert
        Assert.That(actual, Is.EqualTo(3));
    }

    [Test]
    public void TestHeight()
    {
        // Arrange
        Rect sut = new(new Point2d(1, 2), new Point2d(3, 5));

        // Act
        long actual = sut.Height;

        // Assert
        Assert.That(actual, Is.EqualTo(4));
    }
}
