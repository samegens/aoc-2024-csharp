using AoC;

namespace SolverTests;

public class RuleTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string input = "47|53";
        Rule expected = new(47, 53);

        // Act
        Rule result = Rule.Parse(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}