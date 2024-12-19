namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        Onsen onsen = Onsen.Parse(lines);
        return onsen.Designs
            .Count(onsen.IsDesignPossible);
    }

    public long SolvePart2()
    {
        Onsen onsen = Onsen.Parse(lines);
        return onsen.Designs
            .Sum(onsen.GetNrWaysToCreateDesign);
    }
}
