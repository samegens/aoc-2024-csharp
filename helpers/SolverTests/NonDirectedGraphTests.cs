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
}
