namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        return lines
            .Select(Equation.Parse)
            .Where(e => e.CanBeSolved())
            .Sum(e => e.TestValue);
    }

    public long SolvePart2()
    {
        return lines
            .Select(Equation.Parse)
            .Where(e => e.CanBeSolvedWithConcatenation())
            .Sum(e => e.TestValue);
    }
}
