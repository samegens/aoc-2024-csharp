



namespace AoC;

public class Robots(List<Robot> robots)
{
    public static Robots Parse(List<string> lines)
    {
        return new Robots(lines.Select(Robot.Parse).ToList());
    }

    public void Print()
    {
        for (int y = 0; y < Robot.AreaHeight; y++)
        {
            for (int x = 0; x < Robot.AreaWidth; x++)
            {
                Point2dL p = new(x, y);
                int count = GetNrRobotsAt(p);
                char ch = count > 0 ? (char)('0' + count) : '.';
                Console.Write(ch);
            }
            Console.WriteLine();
        }
    }

    public void Move(int nrMoves)
    {
        List<Robot> newRobots = robots.Select(r => r.Move(nrMoves)).ToList();
        robots.Clear();
        robots.AddRange(newRobots);
    }

    private int GetNrRobotsAt(Point2dL p)
    {
        return robots.Count(r => r.Position == p);
    }

    public int GetSafetyScore()
    {
        return robots
            .Where(r => r.Quadrant > 0)
            .GroupBy(r => r.Quadrant)
            .Select(g => g.Count())
            .Aggregate(1, (product, count) => product * count);
    }

    public bool IsPotentialChrismasTree()
    {
        // Search for empty space in topleft corner.
        int count = robots.Count(r => r.Position.X + r.Position.Y < Robot.AreaWidth / 2);
        // Console.WriteLine(count);
        return count < 30;
    }
}
