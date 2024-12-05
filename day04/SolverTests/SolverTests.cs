using AoC;

namespace SolverTests;

public class Tests
{
    [Test]
    public void TestFindAll4LetterWordsHorizontal()
    {
        // Arrange
        List<string> input = [.. """
        XMAS
        """.Split('\n')];
        Solver sut = new(input);
        List<string> expected = ["XMAS", "SAMX"];

        // Act
        List<string> result = sut.FindAll4LetterWords();

        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        MMMSXXMASM
        MSAMXMSMSA
        AMXSXMAAMM
        MSAMASMSMX
        XMASAMXAMM
        XXAMMXXAMA
        SMSMSASXSS
        SAXAMASAAA
        MAMMMXMMMM
        MXMXAXMASX
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(18));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        List<string> input = [.. """
        MMMSXXMASM
        MSAMXMSMSA
        AMXSXMAAMM
        MSAMASMSMX
        XMASAMXAMM
        XXAMMXXAMA
        SMSMSASXSS
        SAXAMASAAA
        MAMMMXMMMM
        MXMXAXMASX
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(9));
    }
}
