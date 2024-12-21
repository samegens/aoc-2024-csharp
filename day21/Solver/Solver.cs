namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        return lines.
            Sum(s => Keypad.GetHumanSequenceLength(s, nrRobots: 3) * int.Parse(s.Trim('A')));
    }

    public int SolvePart2()
    {
        return 0;
    }
}
