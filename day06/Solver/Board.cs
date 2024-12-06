namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Point GuardPos { get; private set; }
    public Point OriginalGuardPos { get; private set; }
    public Direction GuardDirection { get; private set; }
    public HashSet<Point> VisitedPositions { get; private set; } = [];

    public Board(List<string> lines)
    {
        Width = lines[0].Trim().Length;
        Height = lines.Count;
        _tiles = new char[Width, Height];

        // To keep the compiler happy:
        GuardPos = new Point(0, 0);

        for (int y = 0; y < Height; y++)
        {
            string line = lines[y].Trim();
            for (int x = 0; x < Width; x++)
            {
                char ch = line[x];
                _tiles[x, y] = ch;
                if (ch == '^')
                {
                    GuardPos = new Point(x, y);
                }
            }
        }

        GuardDirection = Direction.Up;
        OriginalGuardPos = GuardPos;
        VisitedPositions.Add(GuardPos);
    }

    public char this[int x, int y] => _tiles[x, y];

    public char this[Point p] => _tiles[p.X, p.Y];

    public bool Contains(Point p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

    public void MoveOneStep()
    {
        Point newPosition = GuardPos.Move(GuardDirection);
        if (ContainsObstacle(newPosition))
        {
            TurnRight();
        }
        else
        {
            GuardPos = newPosition;
            if (Contains(GuardPos))
            {
                VisitedPositions.Add(GuardPos);
            }
        }
    }

    private void TurnRight()
    {
        Dictionary<Direction, Direction> turns = new()
        {
            [Direction.Up] = Direction.Right,
            [Direction.Right] = Direction.Down,
            [Direction.Down] = Direction.Left,
            [Direction.Left] = Direction.Up
        };
        GuardDirection = turns[GuardDirection];
    }

    public bool WillObstacleCreateLoop(Point point)
    {
        // We're not allowed to put an obstacle at the starting point of the guard.
        if (point == OriginalGuardPos)
        {
            return false;
        }

        try
        {
            _tiles[point.X, point.Y] = '#';
            ResetGuard();
            HashSet<(Point, Direction)> VisitedStates = [(GuardPos, GuardDirection)];
            do
            {
                MoveOneStep();
                if (VisitedStates.Contains((GuardPos, GuardDirection)))
                {
                    return true;
                }
                VisitedStates.Add((GuardPos, GuardDirection));
            }
            while (Contains(GuardPos));
            return false;
        }
        finally
        {
            // Remove the obstacle for the next call to WillObstacleCreateLoop.
            _tiles[point.X, point.Y] = '.';
        }
    }

    public void Print()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                char ch = _tiles[x, y];
                if (x == GuardPos.X && y == GuardPos.Y)
                {
                    ch = DirectionHelpers.Chars[GuardDirection];
                }
                Console.Write(ch);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private bool ContainsObstacle(Point position) => Contains(position) && this[position] == '#';

    private void ResetGuard()
    {
        GuardPos = OriginalGuardPos;
        GuardDirection = Direction.Up;
        VisitedPositions = [GuardPos];
    }
}
