using AoC;

namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        Register A: 729
        Register B: 0
        Register C: 0

        Program: 0,1,5,4,3,0
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        string result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
    }
}
