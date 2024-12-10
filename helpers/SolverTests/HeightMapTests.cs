using AoC;

namespace SolverTests;

public class HeightMapTests
{
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
}
