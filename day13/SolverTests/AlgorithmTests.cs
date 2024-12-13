using AoC;

namespace SolverTests;

public class AlgorithmTests
{
    [Test]
    public void TestSolveLinearEquations2x2()
    {
        // Act
        (bool hasSolution, long actualX, long actualY) = Algorithms.SolveLinearEquations2x2(94, 22, 8400, 34, 67, 5400);

        // Assert
        Assert.That(hasSolution, Is.True);
        Assert.That(actualX, Is.EqualTo(80));
        Assert.That(actualY, Is.EqualTo(40));
    }
}