using AoC;

namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        string input = "125 17";
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(55312));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        string input = "125 17";
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(65601038650482));
    }
}
