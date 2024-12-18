using AoC;

namespace SolverTests;

public class DirectionsTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> lines = """
        ^>
        v<
        """.Split('\n').ToList();
        List<Direction> expected = [Direction.Up, Direction.Right, Direction.Down, Direction.Left];

        // Act
        List<Direction> actual = DirectionHelpers.Parse(lines);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
