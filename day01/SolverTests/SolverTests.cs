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
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(11));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        List<string> input = [.. """
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(31));
    }

    [TestCase("3   4", 3, 4)]
    [TestCase("  3   4  ", 3, 4)]
    public void TestParseLine(string line, int expectedLeftNumber, int expectedRightNumber)
    {
        // Act
        (int leftNumber, int rightNumber) = Solver.ParseLine(line);

        // Assert
        Assert.That(leftNumber, Is.EqualTo(expectedLeftNumber));
        Assert.That(rightNumber, Is.EqualTo(expectedRightNumber));
    }
}
