using AoC;

namespace SolverTests;

public class RectTests
{
    [Test]
    public void TestSingleEdgeTouchesInnerRect()
    {
        // Arrange
        Rect sut = new(new Point2dI(1, 1), new Point2dI(4, 5));
        Rect inner = new(new Point2dI(2, 2), new Point2dI(3, 4));

        // Act
        bool actual = sut.SingleEdgeTouches(inner);

        // Assert
        Assert.That(actual, Is.False);
    }

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
}
