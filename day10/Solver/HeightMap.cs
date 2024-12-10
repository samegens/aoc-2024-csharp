

namespace AoC;

public class HeightMap
{
    private readonly int[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public HeightMap(List<string> lines)
    {
        Width = lines[0].Trim().Length;
        Height = lines.Count;
        _tiles = new int[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            string line = lines[y].Trim();
            for (int x = 0; x < Width; x++)
            {
                _tiles[x, y] = line[x] - '0';
            }
        }
    }

    public int this[int x, int y] => _tiles[x, y];

    public int this[Point2dI p] => _tiles[p.X, p.Y];

    public bool Contains(Point2dI p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

    public int ComputeTotalScore()
    {
        return Points.Sum(ComputeScore);
    }

    public int ComputeScore(Point2dI p)
    {
        if (this[p] != 0)
        {
            return 0;
        }

        HashSet<Point2dI> reachedTops = [];
        HashSet<Point2dI> visitedPoints = [p];
        FindTops(p, reachedTops, visitedPoints);
        return reachedTops.Count;
    }

    private void FindTops(Point2dI p, HashSet<Point2dI> reachedTops, HashSet<Point2dI> visitedPoints)
    {
        if (this[p] == 9)
        {
            reachedTops.Add(p);
        }

        foreach ((_, Point2dI delta) in DirectionHelpers.Movements)
        {
            int level = this[p];
            Point2dI newP = p + delta;
            if (Contains(newP) && this[newP] == level + 1 && !visitedPoints.Contains(newP))
            {
                visitedPoints.Add(newP);
                FindTops(newP, reachedTops, visitedPoints);
            }
        }
    }

    public void Print()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                char ch = (char)(_tiles[x, y] + '0');
                Console.Write(ch);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public int ComputeTotalRating()
    {
        return Points.Sum(ComputeRating);
    }

    public int ComputeRating(Point2dI p)
    {
        if (this[p] != 0)
        {
            return 0;
        }

        List<Point2dI> reachedTops = [];
        FindTopsViaAllTrails(p, reachedTops);
        return reachedTops.Count;
    }

    private void FindTopsViaAllTrails(Point2dI p, List<Point2dI> reachedTops)
    {
        if (this[p] == 9)
        {
            reachedTops.Add(p);
        }

        foreach ((_, Point2dI delta) in DirectionHelpers.Movements)
        {
            int level = this[p];
            Point2dI newP = p + delta;
            if (Contains(newP) && this[newP] == level + 1)
            {
                FindTopsViaAllTrails(newP, reachedTops);
            }
        }
    }

    private IEnumerable<Point2dI> Points
    {
        get
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    yield return new Point2dI(x, y);
                }
            }
        }
    }
}
