using AoC;

namespace SolverTests;

public class SolverTests
{
    private static readonly List<string> input = [.. """
        029A
        980A
        179A
        456A
        379A
        """.Split('\n')];

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(126384));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(175396398527088));
    }
}
