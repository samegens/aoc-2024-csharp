using AoC;

namespace SolverTests;

public class ReportTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string input = "1 2";
        List<int> expected = [1, 2];

        // Act
        List<int> result = Report.Parse(input).Levels;

        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }

    [TestCase("1 2", true)]
    [TestCase("1 1", false)]
    [TestCase("2 1", true)]
    [TestCase("1 2 1", false)]
    [TestCase("2 1 2", false)]
    [TestCase("1 2 5", true)]
    [TestCase("1 2 6", false)]
    public void TestIsSafe(string line, bool expected)
    {
        // Arrange
        Report sut = Report.Parse(line);

        // Act
        bool result = sut.IsSafe;

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("1 2", true)]
    [TestCase("1 5 9", false)]
    public void TestIsSafeWithProblemDampener(string line, bool expected)
    {
        // Arrange
        Report sut = Report.Parse(line);

        // Act
        bool result = sut.IsSafeWithProblemDampener;

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
