namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        return new HeightMap(lines)
            .ComputeTotalScore();
    }

    public int SolvePart2()
    {
        return new HeightMap(lines)
            .ComputeTotalRating();
    }
}
