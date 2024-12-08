using AoC;

namespace SolverTests;

public class BoardTests
{
    [Test]
    public void TestGenerateFrequencyLists()
    {
        // Arrange
        List<string> input = """
        .0A.
        A..0
        """.Split('\n').ToList();
        Board sut = new(input);
        Dictionary<char, List<Point>> expected = new()
        {
            ['0'] = [new Point(1, 0), new Point(3, 1)],
            ['A'] = [new Point(2, 0), new Point(0, 1)]
        };

        // Act
        Dictionary<char, List<Point>> actual = sut.GenerateFrequencyLists();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
