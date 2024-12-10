using AoC;

namespace SolverTests;

public class Point2dTests
{
    [Test]
    public void TestMoveInt()
    {
        // Arrange
        Point2d<int> sut = new(1, 2);
        Point2d<int> expected = new(4, 6);

        // Act
        Point2d<int> actual = sut.Move(3, 4);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveLong()
    {
        // Arrange
        Point2d<long> sut = new((long)int.MaxValue - 1, (long)int.MaxValue - 2);
        Point2d<long> expected = new((long)int.MaxValue + 3, (long)int.MaxValue + 4);

        // Act
        Point2d<long> actual = sut.Move(4, 6);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
