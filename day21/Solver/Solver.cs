namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        return lines.
            Sum(s => new Keypad(3).GetHumanSequenceLength(s) * int.Parse(s.Trim('A')));
    }

    public long SolvePart2()
    {
        return lines.
            Sum(s => new Keypad(25).GetHumanSequenceLength(s) * int.Parse(s.Trim('A')));
    }
}
