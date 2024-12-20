using AoC;

namespace SolverTests;

public class LineParserTests
{
    [TestCase("aaa12bbb34ccc", 12, 34)]
    [TestCase("12bbb34", 12, 34)]
    [TestCase("aaa-12bbb-34ccc", -12, -34)]
    [TestCase("-12bbb-34", -12, -34)]
    public void TestParse2Ints(string input, int expected1, int expected2)
    {
        // Act
        (int actual1, int actual2) = LineParser.Parse2Ints(input);

        // Assert
        Assert.That(actual1, Is.EqualTo(expected1));
        Assert.That(actual2, Is.EqualTo(expected2));
    }

    [TestCase("aaa12bbb34ccc56dd", 12, 34, 56)]
    [TestCase("12bbb34ccc56", 12, 34, 56)]
    [TestCase("aaa-12bbb-34cccddd-56", -12, -34, -56)]
    [TestCase("-12bbb-34ccc-56", -12, -34, -56)]
    public void TestParse3(string input, int expected1, int expected2, int expected3)
    {
        // Act
        (int actual1, int actual2, int actual3) = LineParser.Parse3Ints(input);

        // Assert
        Assert.That(actual1, Is.EqualTo(expected1));
        Assert.That(actual2, Is.EqualTo(expected2));
        Assert.That(actual3, Is.EqualTo(expected3));
    }

    [TestCase("aaa12bbb34ccc56ddd78eee", 12, 34, 56, 78)]
    [TestCase("12bbb34ccc56ddd78", 12, 34, 56, 78)]
    [TestCase("aaa-12bbb-34ccc-56ddd-78eee", -12, -34, -56, -78)]
    [TestCase("-12bbb-34ccc-56ddd-78", -12, -34, -56, -78)]
    public void TestParse3(string input, int expected1, int expected2, int expected3, int expected4)
    {
        // Act
        (int actual1, int actual2, int actual3, int actual4) = LineParser.Parse4Ints(input);

        // Assert
        Assert.That(actual1, Is.EqualTo(expected1));
        Assert.That(actual2, Is.EqualTo(expected2));
        Assert.That(actual3, Is.EqualTo(expected3));
        Assert.That(actual4, Is.EqualTo(expected4));
    }
}
