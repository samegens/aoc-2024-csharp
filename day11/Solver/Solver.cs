namespace AoC;

public class Solver(string line)
{
    public long SolvePart1()
    {
        Stones stones = Stones.Parse(line);
        stones.Blink(25);
        return stones.TotalStoneCount;
    }

    public long SolvePart2()
    {
        Stones stones = Stones.Parse(line);
        stones.Blink(75);
        return stones.TotalStoneCount;
    }
}
