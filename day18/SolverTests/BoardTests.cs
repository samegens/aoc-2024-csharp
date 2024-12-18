using AoC;

namespace SolverTests;

public class BoardTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        5,4
        4,2
        4,5
        3,0
        2,1
        6,3
        2,4
        1,5
        0,6
        3,3
        2,6
        5,1
        1,2
        5,5
        2,5
        6,5
        1,4
        0,4
        6,4
        1,1
        6,1
        1,0
        0,5
        1,6
        2,0
        """.Split('\n').ToList();
        string expected = """
        ...#...
        ..#..#.
        ....#..
        ...#..#
        ..#..#.
        .#..#..
        #.#....

        """;

        // Act
        Board sut = Board.Parse(7, 7, input, 12);

        // Assert
        Assert.That(sut.ToString, Is.EqualTo(expected));
    }

    [Test]
    public void TestGetShortestPath()
    {
        // Arrange
        List<string> input = """
        ...#...
        ..#..#.
        ....#..
        ...#..#
        ..#..#.
        .#..#..
        #.#....
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        int result = sut.GetShortestPath();

        // Assert
        Assert.That(result, Is.EqualTo(22));
    }

    [Test]
    public void TestGetPosThatBlocksExit()
    {
        // Arrange
        List<string> input = """
        5,4
        4,2
        4,5
        3,0
        2,1
        6,3
        2,4
        1,5
        0,6
        3,3
        2,6
        5,1
        1,2
        5,5
        2,5
        6,5
        1,4
        0,4
        6,4
        1,1
        6,1
        1,0
        0,5
        1,6
        2,0
        """.Split('\n').ToList();
        Board sut = new(7, 7);

        // Act
        Point2d result = sut.GetPosThatBlocksExit(input);

        // Assert
        Assert.That(result, Is.EqualTo(new Point2d(6, 1)));
    }

}
