using AoC;

namespace SolverTests;

public class SolverTests
{

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        1
        10
        100
        2024
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(37327623));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        List<string> input = [.. """
        1
        2
        3
        2024
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(23));
    }
}
