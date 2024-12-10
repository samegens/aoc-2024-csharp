using AoC;

namespace SolverTests;

public class SolverTests
{
    static readonly List<string> input = [.. """
        89010123
        78121874
        87430965
        96549874
        45678903
        32019012
        01329801
        10456732
        """.Split('\n')];

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(36));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(81));
    }
}
