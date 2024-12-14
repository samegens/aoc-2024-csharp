using AoC;

namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        p=0,4 v=3,-3
        p=6,3 v=-1,-3
        p=10,3 v=-1,2
        p=2,0 v=2,-1
        p=0,0 v=1,3
        p=3,0 v=-2,-2
        p=7,6 v=-1,-3
        p=3,0 v=-1,-2
        p=9,3 v=2,3
        p=7,3 v=-1,2
        p=2,4 v=2,-3
        p=9,5 v=-3,-3
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(12));
    }

    // No unit test for part 2? Nope, too hard, not worth it.
}
