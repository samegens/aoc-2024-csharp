using AoC;

namespace SolverTests;

public class EquationTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string line = "2400257423699: 506 6 8 47 7 473 3 699";
        Equation expected = new(2400257423699, [506, 6, 8, 47, 7, 473, 3, 699]);

        // Act
        Equation actual = Equation.Parse(line);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("190: 10 19", true)]
    [TestCase("161011: 16 10 13", false)]
    public void TestCanBeSolved(string line, bool expected)
    {
        // Arrange
        Equation sut = Equation.Parse(line);

        // Act
        bool actual = sut.CanBeSolved();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("190: 10 19", true)]
    [TestCase("161011: 16 10 13", false)]
    [TestCase("7290: 6 8 6 15", true)]
    public void TestCanBeSolvedWithConcatenation(string line, bool expected)
    {
        // Arrange
        Equation sut = Equation.Parse(line);

        // Act
        bool actual = sut.CanBeSolvedWithConcatenation();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
