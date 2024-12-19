using AoC;

namespace SolverTests;

public class OnsenTests
{
    private static readonly List<string> input = """
        r, wr, b, g, bwu, rb, gb, br

        brwrr
        bggr
        gbbr
        rrbgbr
        ubwu
        bwurrg
        brgr
        bbrgwb
        """.Split('\n').ToList();

    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> expectedTowels = ["r", "wr", "b", "g", "bwu", "rb", "gb", "br"];
        List<string> expectedDesigns = [
            "brwrr",
            "bggr",
            "gbbr",
            "rrbgbr",
            "ubwu",
            "bwurrg",
            "brgr",
            "bbrgwb",
        ];

        // Act
        Onsen result = Onsen.Parse(input);

        // Assert
        Assert.That(result.Towels, Is.EqualTo(expectedTowels));
        Assert.That(result.Designs, Is.EqualTo(expectedDesigns));
    }

    [TestCase("brwrr", true)]
    public void TestFunction(string design, bool expected)
    {
        // Arrange
        Onsen sut = Onsen.Parse(input);

        // Act
        bool actual = sut.IsDesignPossible(design);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("brwrr", 2)]
    [TestCase("rrbgbr", 6)]
    public void TestFunction(string design, int expectedNrWays)
    {
        // Arrange
        Onsen sut = Onsen.Parse(input);

        // Act
        long actual = sut.GetNrWaysToCreateDesign(design);

        // Assert
        Assert.That(actual, Is.EqualTo(expectedNrWays));
    }

}
