using AoC;

namespace SolverTests;

public class Point2dTests
{
    [Test]
    public void TestMoveInt()
    {
        // Arrange
        Point2d sut = new(1, 2);
        Point2d expected = new(4, 6);

        // Act
        Point2d actual = sut.Move(3, 4);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveLong()
    {
        // Arrange
        Point2d sut = new((long)int.MaxValue - 1, (long)int.MaxValue - 2);
        Point2d expected = new((long)int.MaxValue + 3, (long)int.MaxValue + 4);

        // Act
        Point2d actual = sut.Move(4, 6);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(0, 0, 3, 4, 7)]
    [TestCase(3, 4, 0, 0, 7)]
    [TestCase(-3, -4, 5, 6, 18)]
    [TestCase(5, 6, -3, -4, 18)]
    public void TestManhattanDistanceTo(int x1, int y1, int x2, int y2, long expectedDistance)
    {
        // Arrange
        Point2d sut = new(x1, y1);
        Point2d p2 = new(x2, y2);

        // Act
        long actual = sut.ManhattanDistanceTo(p2);

        // Assert
        Assert.That(actual, Is.EqualTo(expectedDistance));
    }
}
