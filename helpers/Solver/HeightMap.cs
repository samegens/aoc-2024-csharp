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

    public IEnumerable<Point2dI> Points
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
}
