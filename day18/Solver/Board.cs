
using System.Text;

namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        _tiles = new char[Width, Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _tiles[x, y] = '.';
            }
        }
    }

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

    public void SetAt(int x, int y, char ch)
    {
        _tiles[x, y] = ch;
    }

    public char this[int x, int y] => _tiles[x, y];

    public char this[Point2d p] => _tiles[p.X, p.Y];

    public bool Contains(Point2d p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

    public bool CanBeEntered(Point2d p) => Contains(p) && this[p] == '.';

    public override string ToString()
    {
        StringBuilder sb = new();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                char ch = _tiles[x, y];
                sb.Append(ch);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static Board Parse(int width, int height, List<string> input, int nrLinesToParse)
    {
        Board board = new(width, height);
        for (int i = 0; i < nrLinesToParse; i++)
        {
            (int x, int y) = ParseLine(input[i]);
            board.SetAt(x, y, '#');
        }
        return board;
    }

    private static (int x, int y) ParseLine(string line)
    {
        string[] parts = line.Trim().Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public int GetShortestPath()
    {
        Point2d start = new(0, 0);
        Point2d end = new(Width - 1, Height - 1);
        NonDirectedGraph graph = CreateNonDirectedGraph(start);
        return graph.ComputeShortestPathFromStartToEnd(start, end);
    }

    public NonDirectedGraph CreateNonDirectedGraph(Point2d start)
    {
        NonDirectedGraph graph = new();
        HashSet<Point2d> visitedTiles = [];
        AddTileToGraph(start, graph, visitedTiles);
        return graph;
    }

    private void AddTileToGraph(Point2d p, NonDirectedGraph graph, HashSet<Point2d> visitedTiles)
    {
        GraphNode thisNode = new(p);
        graph.AddVertex(thisNode);
        visitedTiles.Add(p);

        const int movementCost = 1;

        foreach ((_, Point2d delta) in DirectionHelpers.Movements)
        {
            Point2d newP = p.Move(delta);
            if (CanBeEntered(newP))
            {
                if (!visitedTiles.Contains(newP))
                {
                    AddTileToGraph(newP, graph, visitedTiles);
                }
                GraphNode nextNode = graph.GetAt(newP);
                graph.AddEdge(thisNode, nextNode, movementCost);
            }
        }
    }

    public Point2d GetPosThatBlocksExit(List<string> input)
    {
        Board board = new(Width, Height);
        Point2d start = new(0, 0);
        Point2d end = new(Width - 1, Height - 1);
        for (int i = 0; i < input.Count; i++)
        {
            (int x, int y) = ParseLine(input[i]);
            board.SetAt(x, y, '#');

            // We know from the puzzle that the first 1024 bytes will not
            // cause the exit to become unreachable.
            // I could have implemented binary search for the rest, but
            // since the solution was found after a couple of seconds, I
            // didn't bother.
            if (Width <= 7 || i >= 1024)
            {
                NonDirectedGraph graph = board.CreateNonDirectedGraph(start);
                if (!graph.Contains(end))
                {
                    return new Point2d(x, y);
                }
            }
        }

        return new Point2d(0, 0);
    }
}
