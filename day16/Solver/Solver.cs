namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        Board board = new(lines);
        (int shortestPath, _) = board.GetShortestPath();
        return shortestPath;
    }

    public int SolvePart2()
    {
        Board board = new(lines);
        (_, int nrTilesOnShortestPaths) = board.GetShortestPath();
        return nrTilesOnShortestPaths;
    }
}
