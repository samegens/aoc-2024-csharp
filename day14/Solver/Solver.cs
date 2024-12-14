namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        Robots robots = Robots.Parse(lines);
        robots.Move(100);
        return robots.GetSafetyScore();
    }

    public int SolvePart2()
    {
        Robots robots = Robots.Parse(lines);
        for (int i = 0; i < 100000; i++)
        {
            robots.Move(1);
            if (robots.IsPotentialChrismasTree())
            {
                return i + 1;
            }
        }
        return 0;
    }
}
