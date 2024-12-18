using AoC;

namespace SolverTests;

public class SolverTests
{
    private static readonly List<string> input = """
        5,4
        4,2
        4,5
        3,0
        2,1
        6,3
        2,4
        1,5
        0,6
        3,3
        2,6
        5,1
        1,2
        5,5
        2,5
        6,5
        1,4
        0,4
        6,4
        1,1
        6,1
        1,0
        0,5
        1,6
        2,0
        """.Split('\n').ToList();

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver sut = new(input, 7, 7, 12);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(22));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        Solver sut = new(input, 7, 7, 12);

        // Act
        string result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo("6,1"));
    }
}
