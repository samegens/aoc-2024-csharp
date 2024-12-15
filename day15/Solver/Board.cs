using System.Text;

namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Point2d RobotPosition { get; private set; }

    public Board(List<string> lines)
    {
        RobotPosition = new(0, 0);
        Width = lines[0].Trim().Length;
        Height = lines.Count;
        _tiles = new char[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            string line = lines[y].Trim();
            for (int x = 0; x < Width; x++)
            {
                char ch = line[x];
                if (ch == '@')
                {
                    RobotPosition = new(x, y);
                    _tiles[x, y] = '.';
                }
                else
                {
                    _tiles[x, y] = ch;
                }
            }
        }
    }

    public char this[int x, int y] => _tiles[x, y];

    public char this[Point2d p] => _tiles[p.X, p.Y];

    public bool Contains(Point2d p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

    public override string ToString()
    {
        StringBuilder sb = new();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (x == RobotPosition.X && y == RobotPosition.Y)
                {
                    sb.Append('@');
                }
                else
                {
                    sb.Append(_tiles[x, y]);
                }
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void Print()
    {
        Console.WriteLine(ToString());
    }

    public static (Board board, List<Direction> directions) ParseBoardAndDirections(List<string> input)
    {
        int separatorIndex = input.IndexOf(string.Empty);
        return (new Board(input[0..separatorIndex]), DirectionHelpers.Parse(input[(separatorIndex + 1)..]));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Board other = (Board)obj;
        if (other.Width != Width || other.Height != Height || other.RobotPosition != RobotPosition)
        {
            return false;
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (other[x, y] != _tiles[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Width, Height, _tiles);
    }

    public void MoveRobotTo(Direction direction)
    {
        Point2d newPos = RobotPosition.Move(direction);
        if (this[newPos] == '#')
        {
            return;
        }

        if (this[newPos] == '.')
        {
            RobotPosition = newPos;
            return;
        }

        // Only boxes left, can we move them?
        Point2d checkPos = newPos.Move(direction);
        while (this[checkPos] == 'O')
        {
            checkPos = checkPos.Move(direction);
        }

        if (this[checkPos] == '#')
        {
            // Nope, we can't.
            return;
        }

        // Yes, we can. We don't need to actually shift the boxes to
        // get a correct representation. Just add a new box on the last
        // position we checked and remove the position the robot moves to.
        _tiles[checkPos.X, checkPos.Y] = 'O';
        _tiles[newPos.X, newPos.Y] = '.';
        RobotPosition = newPos;
    }

    public void MoveRobot(List<Direction> directions)
    {
        foreach (Direction direction in directions)
        {
            MoveRobotTo(direction);
        }
    }

    public long GetGpsScore()
    {
        return GetBoxPositions()
            .Sum(GetGpsScoreForBox);
    }

    private IEnumerable<Point2d> GetBoxPositions()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (this[x, y] == 'O')
                {
                    yield return new Point2d(x, y);
                }
            }
        }
    }

    private long GetGpsScoreForBox(Point2d position)
    {
        return position.Y * 100 + position.X;
    }
}
