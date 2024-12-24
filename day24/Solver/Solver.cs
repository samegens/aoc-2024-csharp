namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        return Device.Parse(lines).ProduceNumber();
    }

    public int SolvePart2()
    {
        return 0;
    }
}
