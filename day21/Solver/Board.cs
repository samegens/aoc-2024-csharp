using System.Text;

namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Point2d Start { get; private set; } = new Point2d(-1, -1);
    public Point2d End { get; private set; } = new Point2d(-1, -1);
    public Point2d Current { get; set; } = new Point2d(-1, -1);

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
                else if (line[x] == 'A')
                {
                    Current = new Point2d(x, y);
                }
            }
        }
    }

    public char this[int x, int y] => _tiles[x, y];

    public char this[Point2d p] => _tiles[p.X, p.Y];

    public bool Contains(Point2d p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

    public bool CanBeEntered(Point2d p) => Contains(p) && this[p] != '#';

    public override string ToString()
    {
        StringBuilder sb = new();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Start.X == x && Start.Y == x)
                {
                    sb.Append('S');
                }
                else if (End.X == x && End.Y == y)
                {
                    sb.Append('E');
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

    internal string ToString(int y)
    {
        if (y >= Height)
        {
            return "    ";
        }

        string row = "";
        for (int x = 0; x < Width; x++)
        {
            if (y == Current.Y && x == Current.X)
            {
                row += '@';
            }
            else
            {
                row += this[x, y];
            }
        }
        if (Width < 4)
        {
            row += new string(' ', 4 - Width);
        }

        return row;
    }
}
