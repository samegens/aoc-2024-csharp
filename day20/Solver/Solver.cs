namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1(int minSavings)
    {
        Board board = new(lines);
        return board.Path.Keys
            .SelectMany(board.GetShortcutsAt)
            .Count(s => s >= minSavings);
    }

    public int SolvePart2(int minSavings)
    {
        int nrShortcutsWithMinSavings = 0;
        Board board = new(lines);

        // This is O(n2), but since we're doing cheap checks it's not too bad,
        // it takes 4 seconds to process the complete input on my laptop.
        foreach (Point2d p1 in board.Path.Keys)
        {
            foreach (Point2d p2 in board.Path.Keys)
            {
                if (p2 != p1)
                {
                    if (board.GetTimeSavedBetween(p1, p2) >= minSavings)
                    {
                        nrShortcutsWithMinSavings++;
                    }
                }
            }
        }

        return nrShortcutsWithMinSavings;
    }
}
