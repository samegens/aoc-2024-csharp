namespace AoC;

public class Solver(List<string> lines, int boardWidth, int boardHeight, int nrLinesToParse)
{
    public int SolvePart1()
    {
        Board board = Board.Parse(boardWidth, boardHeight, lines, nrLinesToParse);
        return board.GetShortestPath();
    }

    public string SolvePart2()
    {
        Board board = new(boardWidth, boardHeight);
        Point2d p = board.GetPosThatBlocksExit(lines);
        return $"{p.X},{p.Y}";
    }
}
