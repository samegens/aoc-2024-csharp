global using Point2dL = AoC.Point2d<long>;

using AoC;

namespace SolverTests;

public class RobotTests
{
    [SetUp]
    public void SetUp()
    {
        Robot.AreaWidth = 11;
        Robot.AreaHeight = 7;
    }

    [Test]
    public void TestParse()
    {
        // Arrange
        string input = "p=6,3 v=-1,-3";
        Robot expected = new(new Point2dL(6, 3), new Point2dL(-1, -3));

        // Act
        Robot robot = Robot.Parse(input);

        // Assert
        Assert.That(robot, Is.EqualTo(expected));
    }

    [TestCase(2, 4, 2, -3, 1, 4, 1)]
    [TestCase(2, 4, 2, -3, 2, 6, 5)]
    [TestCase(2, 4, 2, -3, 4, 10, 6)]
    [TestCase(10, 6, 2, -3, 1, 1, 3)]
    [TestCase(2, 4, 2, -3, 5, 1, 3)]
    [TestCase(0, 0, -1, -1, 1, 10, 6)]
    [TestCase(10, 6, 1, 1, 1, 0, 0)]
    public void TestMove(long startX, long startY, long vX, long vY, long nrMoves, long expectedX, long expectedY)
    {
        // Arrange
        Robot sut = new(new Point2dL(startX, startY), new Point2dL(vX, vY));
        Robot expected = new(new Point2dL(expectedX, expectedY), new Point2dL(vX, vY));

        // Act
        Robot actual = sut.Move(nrMoves);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(0, 0, 1)]
    [TestCase(4, 2, 1)]
    [TestCase(5, 0, 0)]
    [TestCase(6, 0, 2)]
    [TestCase(10, 2, 2)]
    [TestCase(5, 4, 0)]
    [TestCase(0, 4, 3)]
    [TestCase(4, 6, 3)]
    [TestCase(6, 4, 4)]
    [TestCase(6, 10, 4)]
    public void TestQuadrant(long x, long y, int expectedQuadrant)
    {
        // Arrange
        Robot sut = new(new Point2dL(x, y), new Point2dL(0, 0));

        // Act
        int actual = sut.Quadrant;

        // Assert
        Assert.That(actual, Is.EqualTo(expectedQuadrant));
    }
}
