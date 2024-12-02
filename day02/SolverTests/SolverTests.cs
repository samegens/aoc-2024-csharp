using AoC;

namespace SolverTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        7 6 4 2 1
        1 2 7 8 9
        9 7 6 2 1
        1 3 2 4 5
        8 6 4 4 1
        1 3 6 7 9
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        List<string> input = [.. """
        7 6 4 2 1
        1 2 7 8 9
        9 7 6 2 1
        1 3 2 4 5
        8 6 4 4 1
        1 3 6 7 9
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(4));
    }

    [Test]
    public void TestParseReports()
    {
        // Arrange
        List<string> input = [.. """
        1 2
        3 4
        """.Split('\n')];
        Solver sut = new(input);
        List<List<int>> expected = [[1, 2], [3, 4]];

        // Act
        List<List<int>> result = Solver.ParseReports(input).Select(r => r.Levels).ToList();

        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }
}
