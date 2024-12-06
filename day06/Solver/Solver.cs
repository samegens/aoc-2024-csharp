namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        Board board = new(lines);
        while (board.Contains(board.GuardPos))
        {
            board.MoveOneStep();
        }
        return board.VisitedPositions.Count;
    }

    public int SolvePart2()
    {
        Board board = new(lines);
        while (board.Contains(board.GuardPos))
        {
            board.MoveOneStep();
        }

        // We need to make a copy of the visited positions first because
        // it will be changed during WillObstacleCreateLoop.
        return board.VisitedPositions
            .ToList()
            .Count(p => board.WillObstacleCreateLoop(p));
    }
}
