


namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Board(List<string> lines)
    {
        Width = lines[0].Trim().Length;
        Height = lines.Count;
        _tiles = new char[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            string line = lines[y].Trim();
            for (int x = 0; x < Width; x++)
            {
                _tiles[x, y] = line[x];
            }
        }
    }

    public char this[int x, int y] => _tiles[x, y];

    public char this[Point2dI p] => _tiles[p.X, p.Y];

    public bool Contains(Point2dI p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

    public void Print()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                char ch = _tiles[x, y];
                Console.Write(ch);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public List<Plot> GetAllPlots()
    {
        List<Plot> plots = [];
        HashSet<Point2dI> visitedPoints = [];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Point2dI p = new(x, y);
                if (visitedPoints.Contains(p))
                {
                    continue;
                }
                Plot plot = GetPlot(p, visitedPoints);
                plots.Add(plot);
            }
        }
        return plots;
    }

    private Plot GetPlot(Point2dI p, HashSet<Point2dI> visitedPoints)
    {
        List<Point2dI> positions = [];
        ExtendPlot(p, positions, visitedPoints);
        return new Plot(positions);
    }

    private void ExtendPlot(Point2dI p, List<Point2dI> positions, HashSet<Point2dI> visitedPoints)
    {
        if (visitedPoints.Contains(p))
        {
            return;
        }

        positions.Add(p);
        visitedPoints.Add(p);

        foreach ((_, Point2dI movement) in DirectionHelpers.Movements)
        {
            Point2dI newP = p.Move(movement);
            if (Contains(newP) && this[newP] == this[p])
            {
                ExtendPlot(newP, positions, visitedPoints);
            }
        }
    }
}
