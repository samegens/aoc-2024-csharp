global using Point2dI = AoC.Point2d<int>;

using AoC;

namespace SolverTests;

public class RectTests
{
    [TestCase(2, 0, true)]
    [TestCase(0, 2, false)]
    public void TestContains(int x, int y, bool expected)
    {
        // Arrange
        Rect sut = new(new Point2dI(0, 0), new Point2dI(2, 1));

        // Act
        bool actual = sut.Contains(new Point2dI(x, y));

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestWidth()
    {
        // Arrange
        Rect sut = new(new Point2dI(1, 2), new Point2dI(3, 5));

        // Act
        int actual = sut.Width;

        // Assert
        Assert.That(actual, Is.EqualTo(3));
    }

    [Test]
    public void TestHeight()
    {
        // Arrange
        Rect sut = new(new Point2dI(1, 2), new Point2dI(3, 5));

        // Act
        int actual = sut.Height;

        // Assert
        Assert.That(actual, Is.EqualTo(4));
    }
}
