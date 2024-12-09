using AoC;

namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        string input = "2333133121414131402";
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(1928));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        string input = "2333133121414131402";
        Solver sut = new(input);

        // Act
        long result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(2858));
    }
}
