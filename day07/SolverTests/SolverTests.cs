using AoC;

namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        190: 10 19
        3267: 81 40 27
        83: 17 5
        156: 15 6
        7290: 6 8 6 15
        161011: 16 10 13
        192: 17 8 14
        21037: 9 7 18 13
        292: 11 6 16 20
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(3749));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        List<string> input = [.. """
        190: 10 19
        3267: 81 40 27
        83: 17 5
        156: 15 6
        7290: 6 8 6 15
        161011: 16 10 13
        192: 17 8 14
        21037: 9 7 18 13
        292: 11 6 16 20
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(11387));
    }
}
