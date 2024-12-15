using System.Text;

namespace AoC;

public class WideBoard
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Point2d RobotPosition { get; private set; }

    public WideBoard(int width, int height)
    {
        RobotPosition = new(0, 0);
        Width = width;
        Height = height;
        _tiles = new char[Width, Height];
    }

    public WideBoard(List<string> lines)
    {
        RobotPosition = new(0, 0);
        Width = lines[0].Trim().Length * 2;
        Height = lines.Count;
        _tiles = new char[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            string line = lines[y].Trim();
            for (int x = 0; x < line.Length; x++)
            {
                char ch = line[x];
                if (ch == '@')
                {
                    RobotPosition = new(x * 2, y);
                    _tiles[x * 2, y] = '.';
                    _tiles[x * 2 + 1, y] = '.';
                }
                else if (ch == 'O')
                {
                    _tiles[x * 2, y] = '[';
                    _tiles[x * 2 + 1, y] = ']';
                }
                else
                {
                    _tiles[x * 2, y] = ch;
                    _tiles[x * 2 + 1, y] = ch;
                }
            }
        }
    }

    public static WideBoard Parse(List<string> lines)
    {
        int width = lines[0].Trim().Length;
        int height = lines.Count;
        WideBoard board = new(width, height);

        for (int y = 0; y < height; y++)
        {
            string line = lines[y].Trim();
            for (int x = 0; x < width; x++)
            {
                char ch = line[x];
                if (ch == '@')
                {
                    board.RobotPosition = new(x, y);
                    board._tiles[x, y] = '.';
                }
                else
                {
                    board._tiles[x, y] = ch;
                }
            }
        }

        return board;
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

    public static (WideBoard WideBoard, List<Direction> directions) ParseWideBoardAndDirections(List<string> input)
    {
        int separatorIndex = input.IndexOf(string.Empty);
        return (new WideBoard(input[0..separatorIndex]), DirectionHelpers.Parse(input[(separatorIndex + 1)..]));
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

        WideBoard other = (WideBoard)obj;
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
        if (direction == Direction.Left || direction == Direction.Right)
        {
            HandleMoveHorizontally(direction, newPos);
        }
        else
        {
            HandleMoveVertically(direction, newPos);
        }
    }

    private void HandleMoveHorizontally(Direction direction, Point2d newPos)
    {
        Point2d checkPos = newPos.Move(direction);
        while (this[checkPos] == '[' || this[checkPos] == ']')
        {
            checkPos = checkPos.Move(direction);
        }
        if (this[checkPos] == '#')
        {
            // Nope, we can't.
            return;
        }
        else
        {
            // Yes, we can.
            while (checkPos != newPos)
            {
                Point2d prevPos = checkPos.MoveOpposite(direction);
                _tiles[checkPos.X, checkPos.Y] = _tiles[prevPos.X, prevPos.Y];
                _tiles[prevPos.X, prevPos.Y] = '.';
                checkPos = prevPos;
            }
            RobotPosition = newPos;
        }
    }

    private void HandleMoveVertically(Direction direction, Point2d newPos)
    {
        Point2d boxPos = GetBoxPosition(newPos);
        if (BoxCanMove(boxPos, direction))
        {
            MoveBox(boxPos, direction);
            RobotPosition = newPos;
        }
    }

    private bool BoxCanMove(Point2d boxPos, Direction direction)
    {
        Point2d newLeftSidePos = boxPos.Move(direction);
        Point2d newRightSidePos = boxPos.Move(Direction.Right).Move(direction);
        if (this[newLeftSidePos] == '#' || this[newRightSidePos] == '#')
        {
            return false;
        }
        if (this[newLeftSidePos] == '.' && this[newRightSidePos] == '.')
        {
            return true;
        }
        if (IsBoxAt(newLeftSidePos))
        {
            Point2d nextBoxPos = GetBoxPosition(newLeftSidePos);
            if (!BoxCanMove(nextBoxPos, direction))
            {
                return false;
            }
        }
        if (IsBoxAt(newRightSidePos))
        {
            Point2d nextBoxPos = GetBoxPosition(newRightSidePos);
            return BoxCanMove(nextBoxPos, direction);
        }

        return true;
    }

    private void MoveBox(Point2d boxPos, Direction direction)
    {
        Point2d newLeftSidePos = boxPos.Move(direction);
        Point2d newRightSidePos = boxPos.Move(Direction.Right).Move(direction);
        if (this[newLeftSidePos] == '#' || this[newRightSidePos] == '#')
        {
            throw new Exception("trying to move box into wall");
        }
        if (IsBoxAt(newLeftSidePos))
        {
            Point2d nextBoxPos = GetBoxPosition(newLeftSidePos);
            MoveBox(nextBoxPos, direction);
        }
        if (IsBoxAt(newRightSidePos))
        {
            Point2d nextBoxPos = GetBoxPosition(newRightSidePos);
            MoveBox(nextBoxPos, direction);
        }
        if (this[newLeftSidePos] == '.' && this[newRightSidePos] == '.')
        {
            _tiles[newLeftSidePos.X, newLeftSidePos.Y] = '[';
            _tiles[newRightSidePos.X, newRightSidePos.Y] = ']';
            _tiles[boxPos.X, boxPos.Y] = '.';
            _tiles[boxPos.X + 1, boxPos.Y] = '.';
        }
        else
        {
            throw new Exception("Should be able to move box after other boxes have moved");
        }
    }

    private bool IsBoxAt(Point2d boxPosition) => this[boxPosition] == '[' || this[boxPosition] == ']';

    private Point2d GetBoxPosition(Point2d newPos)
    {
        return this[newPos] == '[' ? newPos : newPos.Move(Direction.Left);
    }

    public void MoveRobot(List<Direction> directions)
    {
        foreach (Direction direction in directions)
        {
            MoveRobotTo(direction);
            // Console.WriteLine(ToString());
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
                if (this[x, y] == '[')
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
