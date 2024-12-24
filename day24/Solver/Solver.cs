namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        return Device.Parse(lines).ProduceNumber();
    }

    public string SolvePart2()
    {
        return string.Join(',',
            Device.Parse(lines)
            .GetInvalidGates()
            .Select(g => g.WireOut)
            .Order());
    }
}
