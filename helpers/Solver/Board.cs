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
}
