using AoC;

namespace SolverTests;

public class BoardTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        ###############
        #.......#....E#
        #.#.###.#.###.#
        #.....#.#...#.#
        #.###.#####.#.#
        #.#.#.......#.#
        #.#.#####.###.#
        #...........#.#
        ###.#.#####.#.#
        #...#.....#.#.#
        #.#.#.###.#.#.#
        #.....#...#.#.#
        #.###.#.#.#.#.#
        #S..#.....#...#
        ###############
        """.Split('\n').ToList();
        string expected = """
        ###############
        #.......#....E#
        #.#.###.#.###.#
        #.....#.#...#.#
        #.###.#####.#.#
        #.#.#.......#.#
        #.#.#####.###.#
        #...........#.#
        ###.#.#####.#.#
        #...#.....#.#.#
        #.#.#.###.#.#.#
        #.....#...#.#.#
        #.###.#.#.#.#.#
        #S..#.....#...#
        ###############

        """;

        // Act
        Board sut = new(input);

        // Assert
        Assert.That(sut.ToString(), Is.EqualTo(expected));
        Assert.That(sut.StartTilePosition, Is.EqualTo(new Point2d(1, 13)));
        Assert.That(sut.EndTilePosition, Is.EqualTo(new Point2d(13, 1)));
    }

    [Test]
    public void TestGenerateGraphOneStep()
    {
        // Arrange
        List<string> input = """
        ####
        #SE#
        ####
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        DirectedGraph graph = sut.GenerateGraph();

        // Assert
        Assert.That(graph.Count, Is.EqualTo(2));
        List<Edge> edges = graph.GetEdges(graph.GetAt(new Point2d(1, 1), Direction.Right));
        Assert.That(edges.Count, Is.EqualTo(1));
        Assert.That(edges[0].Cost, Is.EqualTo(1));
    }

    [Test]
    public void TestGenerateGraphCorner()
    {
        // Arrange
        List<string> input = """
        ####
        #.E#
        #S##
        ####
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        DirectedGraph graph = sut.GenerateGraph();

        // Assert
        Assert.That(graph.Count, Is.EqualTo(3));
        List<Edge> edges = graph.GetEdges(graph.GetAt(new Point2d(1, 1), Direction.Up));
        List<Edge> expectedEdges = [
            new Edge(graph.GetAt(new Point2d(1, 1), Direction.Up), graph.GetAt(new Point2d(2, 1), Direction.Right), 1001),
        ];
        Assert.That(edges, Is.EquivalentTo(expectedEdges));
    }

    [Test]
    public void TestGenerateGraphMultipleWays()
    {
        // Arrange
        List<string> input = """
        #####
        #..E#
        #S.##
        #####
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        DirectedGraph graph = sut.GenerateGraph();

        // Assert
        Assert.That(graph.Count, Is.EqualTo(10));
    }

    [Test]
    public void TestGetGraphComplexExample()
    {
        // Arrange
        List<string> input = """
        ###############
        #.......#....E#
        #.#.###.#.###.#
        #.....#.#...#.#
        #.###.#####.#.#
        #.#.#.......#.#
        #.#.#####.###.#
        #...........#.#
        ###.#.#####.#.#
        #...#.....#.#.#
        #.#.#.###.#.#.#
        #.....#...#.#.#
        #.###.#.#.#.#.#
        #S..#.....#...#
        ###############
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        DirectedGraph graph = sut.GenerateGraph();

        // Assert
        List<Edge> edges = graph.GetEdges(graph.GetAt(new Point2d(1, 9), Direction.Up));
        List<Edge> expectedEdges = [
            new Edge(graph.GetAt(new Point2d(1, 9), Direction.Up), graph.GetAt(new Point2d(2, 9), Direction.Right), 1001),
        ];
        Assert.That(edges, Is.EquivalentTo(expectedEdges));
    }

    [Test]
    public void TestGetShortestPathLargerExample()
    {
        // Arrange
        List<string> input = """
        ###############
        #.......#....E#
        #.#.###.#.###.#
        #.....#.#...#.#
        #.###.#####.#.#
        #.#.#.......#.#
        #.#.#####.###.#
        #...........#.#
        ###.#.#####.#.#
        #...#.....#.#.#
        #.#.#.###.#.#.#
        #.....#...#.#.#
        #.###.#.#.#.#.#
        #S..#.....#...#
        ###############
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        (int actualShortestPath, int actualNrNodesOnBestPaths) = sut.GetShortestPath();

        // Assert
        Assert.That(actualShortestPath, Is.EqualTo(7036));
        Assert.That(actualNrNodesOnBestPaths, Is.EqualTo(45));
    }

    [Test]
    public void TestGetShortestPathTinyExample()
    {
        // Arrange
        List<string> input = """
        #####
        #..E#
        #S.##
        #####
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        (int actualShortestPath, int actualNrNodesOnBestPaths) = sut.GetShortestPath();

        // Assert
        Assert.That(actualShortestPath, Is.EqualTo(2003));
        Assert.That(actualNrNodesOnBestPaths, Is.EqualTo(5));
    }

}
