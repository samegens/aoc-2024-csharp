
namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        return Machine.ParseMultiple(lines)
            .Select(m => m.Play())
            .Sum();
    }

    public long SolvePart2()
    {
        return Machine.ParseMultiple(lines)
            .Select(m => IncreasePrizePosition(m))
            .Select(m => m.Play())
            .Sum();
    }

    private Machine IncreasePrizePosition(Machine m)
    {
        return new Machine(m.ButtonADelta, m.ButtonBDelta, m.PrizePosition + new Point2dL(10000000000000, 10000000000000));
    }
}
