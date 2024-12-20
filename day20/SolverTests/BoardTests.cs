using AoC;

namespace SolverTests;

public class BoardTests
{
    private static readonly List<string> input = """
        ###############
        #...#...#.....#
        #.#.#.#.#.###.#
        #S#...#.#.#...#
        #######.#.#.###
        #######.#.#...#
        #######.#.###.#
        ###..E#...#...#
        ###.#######.###
        #...###...#...#
        #.#####.#.###.#
        #.#...#.#.#...#
        #.#.#.#.#.#.###
        #...#...#...###
        ###############
        """.Split('\n').ToList();

    [Test]
    public void TestNew()
    {
        // Act
        Board sut = new(input);

        // Assert
        Assert.That(sut.Start, Is.EqualTo(new Point2d(1, 3)));
        Assert.That(sut.End, Is.EqualTo(new Point2d(5, 7)));
    }

    [Test]
    public void TestGetPath()
    {
        // Arrange
        Board sut = new(input);

        // Act
        Dictionary<Point2d, int> result = sut.GetPath();

        // Assert
        Assert.That(result.Count, Is.EqualTo(85));
        Assert.That(result[sut.Start], Is.EqualTo(84));
        Assert.That(result[sut.End], Is.EqualTo(0));
    }

    [TestCase(1, 3, 4)]
    [TestCase(7, 1, 12)]
    public void TestGetShortcutsAt(int x, int y, int expectedSaving)
    {
        // Arrange
        Board sut = new(input);

        // Act
        List<int> result = sut.GetShortcutsAt(new Point2d(x, y));

        // Assert
        List<int> expected = [expectedSaving];
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestGetShortcutsAtWithMultipleShortcuts()
    {
        // Arrange
        Board sut = new(input);

        // Act
        List<int> result = sut.GetShortcutsAt(new Point2d(7, 7));

        // Assert
        List<int> expected = [64, 40];
        Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestGetTimeSavedBetween()
    {
        // Arrange
        Board sut = new(input);

        // Act
        long result = sut.GetTimeSavedBetween(sut.Start, new Point2d(3, 7));

        // Assert
        Assert.That(result, Is.EqualTo(76));
    }
}
