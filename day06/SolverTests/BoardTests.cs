using AoC;

namespace SolverTests;

public class BoardTests
{
    private static readonly List<string> input = """
        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#..^.....
        ........#.
        #.........
        ......#...
        """.Split('\n').ToList();

    [Test]
    public void TestConstructor()
    {
        // Act
        Board sut = new(input);

        // Assert
        Assert.That(sut.GuardDirection, Is.EqualTo(Direction.Up));
        Assert.That(sut.GuardPos, Is.EqualTo(new Point(4, 6)));
        Assert.That(sut.VisitedPositions.Count, Is.EqualTo(1));
    }

    [Test]
    public void TestMoveOneStep()
    {
        // Arrange
        Board sut = new(input);

        // Act
        sut.MoveOneStep();

        // Assert
        Assert.That(sut.GuardPos, Is.EqualTo(new Point(4, 5)));
        Assert.That(sut.GuardDirection, Is.EqualTo(Direction.Up));
    }

    [Test]
    public void TestMoveAndTurn()
    {
        // Arrange
        Board sut = new(input);

        // Act
        for (int i = 0; i < 5; i++)
        {
            sut.MoveOneStep();
        }
        sut.MoveOneStep();

        // Assert
        Assert.That(sut.GuardPos, Is.EqualTo(new Point(4, 1)));
        Assert.That(sut.GuardDirection, Is.EqualTo(Direction.Right));
        Assert.That(sut.VisitedPositions.Count, Is.EqualTo(6));
    }

    [TestCase(3, 6, true)]
    [TestCase(0, 0, false)]
    [TestCase(6, 7, true)]
    public void TestWillObstacleCreateLoop(int x, int y, bool expected)
    {
        // Arrange
        Board sut = new(input);

        // Act
        bool actual = sut.WillObstacleCreateLoop(new Point(x, y));

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}