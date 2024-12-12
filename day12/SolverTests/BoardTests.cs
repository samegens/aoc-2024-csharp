global using Point2dI = AoC.Point2d<int>;
using AoC;

namespace SolverTests;

public class BoardTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        AAAA
        BBCD
        BBCC
        EEEC
        """.Split('\n').ToList();

        // Act
        Board sut = new(input);

        // Assert
        Assert.That(sut.Width, Is.EqualTo(4));
        Assert.That(sut.Height, Is.EqualTo(4));
        Assert.That(sut[0, 0], Is.EqualTo('A'));
        Assert.That(sut[3, 3], Is.EqualTo('C'));
    }

    [Test]
    public void TestGetAllPlots()
    {
        // Arrange
        List<string> input = """
        AAAA
        BBCD
        BBCC
        EEEC
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        List<Plot> actual = sut.GetAllPlots();

        // Assert
        List<int> expectedCounts = [4, 4, 4, 3, 1];
        Assert.That(actual.Select(p => p.Positions.Count), Is.EquivalentTo(expectedCounts));
        Assert.That(actual.Count, Is.EqualTo(5));
        List<Point2dI> expectedPositions = [
            new Point2dI(0, 0),
            new Point2dI(1, 0),
            new Point2dI(2, 0),
            new Point2dI(3, 0)];
        Assert.That(actual[0].Positions, Is.EquivalentTo(expectedPositions));
    }
}
