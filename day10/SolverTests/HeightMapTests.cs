using AoC;

namespace SolverTests;

public class HeightMapTests
{
    static readonly List<string> sampleInput = """
        89010123
        78121874
        87430965
        96549874
        45678903
        32019012
        01329801
        10456732
        """.Split('\n').ToList();

    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        89010123
        78121874
        87430965
        96549874
        45678903
        32019012
        01329801
        """.Split('\n').ToList();

        // Act
        HeightMap sut = new(input);

        // Assert
        Assert.That(sut.Width, Is.EqualTo(8));
        Assert.That(sut.Height, Is.EqualTo(7));
        Assert.That(sut[0, 0], Is.EqualTo(8));
        Assert.That(sut[7, 6], Is.EqualTo(1));
    }

    [TestCase(0, 0, 0)]
    [TestCase(2, 0, 5)]
    public void TestComputeScoreForPoint(int x, int y, int expectedScore)
    {
        // Arrange
        HeightMap sut = new(sampleInput);

        // Act
        int actual = sut.ComputeScore(new Point2d<int>(x, y));

        // Assert
        Assert.That(actual, Is.EqualTo(expectedScore));
    }

    [Test]
    public void TestComputeTotalScore()
    {
        // Arrange
        HeightMap sut = new(sampleInput);

        // Act
        int actual = sut.ComputeTotalScore();

        // Assert
        Assert.That(actual, Is.EqualTo(36));
    }

    [TestCase(0, 0, 0)]
    [TestCase(2, 0, 20)]
    public void TestComputeRating(int x, int y, int expectedScore)
    {
        // Arrange
        HeightMap sut = new(sampleInput);

        // Act
        int actual = sut.ComputeRating(new Point2d<int>(x, y));

        // Assert
        Assert.That(actual, Is.EqualTo(expectedScore));
    }

    [Test]
    public void TestComputeTotalRating()
    {
        // Arrange
        HeightMap sut = new(sampleInput);

        // Act
        int actual = sut.ComputeTotalRating();

        // Assert
        Assert.That(actual, Is.EqualTo(81));
    }

}
