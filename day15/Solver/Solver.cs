namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        (Board board, List<Direction> directions) = Board.ParseBoardAndDirections(lines);
        board.MoveRobot(directions);
        return board.GetGpsScore();
    }

    public long SolvePart2()
    {
        (WideBoard board, List<Direction> directions) = WideBoard.ParseWideBoardAndDirections(lines);
        board.MoveRobot(directions);
        return board.GetGpsScore();
    }
}
