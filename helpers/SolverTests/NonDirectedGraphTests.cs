using AoC;

namespace SolverTests;

public class NonDirectedGraphTests
{
    [Test]
    public void TestToHtml()
    {
        // Arrange
        List<string> boardLines = """
        ####
        #.E#
        #S.#
        ####
        """.Split('\n').ToList();
        Board board = new(boardLines);
        NonDirectedGraph sut = NonDirectedGraph.CreateGraphFromBoard(board);

        // Act
        string actual = sut.ToHtml(board);

        // Assert
        Assert.That(string.IsNullOrEmpty(actual), Is.False);
    }

    [Test]
    public void TestGetShortestPathFromStartToEnd()
    {
        // Arrange
        List<string> input = """
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
        Board board = new(input);
        NonDirectedGraph sut = NonDirectedGraph.CreateGraphFromBoard(board);

        // Act
        int actual = sut.ComputeShortestPathFromStartToEnd(board.Start, board.End);

        // Assert
        Assert.That(actual, Is.EqualTo(84));
    }
}
