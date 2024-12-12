namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        Board board = new(lines);
        List<Plot> plots = board.GetAllPlots();
        return plots.Sum(p => p.Price);
    }

    public int SolvePart2()
    {
        Board board = new(lines);
        List<Plot> plots = board.GetAllPlots();
        return plots.Sum(p => p.BulkDiscountPrice);
    }
}
