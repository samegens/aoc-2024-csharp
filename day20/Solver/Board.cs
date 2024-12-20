

namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    private static readonly List<Point2d> _deltasForShortcuts = [
            new Point2d(0, -2),
            new Point2d(1, -1),
            new Point2d(2, 0),
            new Point2d(1, 1),
            new Point2d(0, 2),
            new Point2d(-1, 1),
            new Point2d(-2, 0),
            new Point2d(-1, -1)
        ];

    public int Width { get; private set; }
    public int Height { get; private set; }
    public Point2d Start { get; private set; } = new Point2d(0, 0);
    public Point2d End { get; private set; } = new Point2d(0, 0);
    public Dictionary<Point2d, int> Path { get; private set; }
    public bool CanBeEntered(Point2d p) => Contains(p) && this[p] != '#';

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
                if (line[x] == 'S')
                {
                    Start = new Point2d(x, y);
                }
                else if (line[x] == 'E')
                {
                    End = new Point2d(x, y);
                }
            }
        }

        Path = GetPath();
    }

    public char this[int x, int y] => _tiles[x, y];

    public char this[Point2d p] => _tiles[p.X, p.Y];

    public bool Contains(Point2d p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

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

    public Dictionary<Point2d, int> GetPath()
    {
        List<Point2d> path = [Start];
        HashSet<Point2d> visitedPoints = [Start];
        Point2d p = Start;
        do
        {
            foreach ((_, Point2d delta) in DirectionHelpers.Movements)
            {
                Point2d newP = p.Move(delta);
                if (CanBeEntered(newP) && !visitedPoints.Contains(newP))
                {
                    path.Add(newP);
                    visitedPoints.Add(newP);
                    p = newP;
                    // Since there's only one path, there are no other directions we can move in.
                    break;
                }
            }
        }
        while (p != End);

        Dictionary<Point2d, int> pointToPathLengthMap = [];
        for (int i = 0; i < path.Count; i++)
        {
            p = path[i];
            int pathLength = path.Count - i - 1;
            pointToPathLengthMap[p] = pathLength;
        }

        return pointToPathLengthMap;
    }

    public List<int> GetShortcutsAt(Point2d p)
    {
        List<int> shortcuts = [];
        foreach (Point2d delta in _deltasForShortcuts)
        {
            Point2d newP = p.Move(delta);
            if (CanBeEntered(newP))
            {
                int newLength = Path[newP];
                if (newLength < Path[p] - 2)
                {
                    int saved = Path[p] - 2 - newLength;
                    shortcuts.Add(saved);
                }
            }
        }
        return shortcuts;
    }

    public long GetTimeSavedBetween(Point2d p1, Point2d p2)
    {
        long manhattanDistance = p1.ManhattanDistanceTo(p2);
        if (manhattanDistance <= 20)
        {
            long saved = Path[p1] - Path[p2] - manhattanDistance;
            if (saved > 0)
            {
                return saved;
            }
        }
        return 0;
    }
}
