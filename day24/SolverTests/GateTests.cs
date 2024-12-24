using AoC;

namespace SolverTests;

public class GateTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string line = "x00 AND y00 -> z00";

        // Act
        Gate sut = Gate.Parse(line);

        // Assert
        Assert.That(sut.WireIn1, Is.EqualTo("x00"));
        Assert.That(sut.WireIn2, Is.EqualTo("y00"));
        Assert.That(sut.Op, Is.EqualTo("AND"));
        Assert.That(sut.WireOut, Is.EqualTo("z00"));
    }
}
