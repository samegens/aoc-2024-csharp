using System.Text;

namespace AoC;

public class Board
{
    private readonly char[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Point2d StartTilePosition { get; private set; }
    public Point2d EndTilePosition { get; private set; }

    public Board(List<string> lines)
    {
        Width = lines[0].Trim().Length;
        Height = lines.Count;
        StartTilePosition = new Point2d(0, 0);
        EndTilePosition = new Point2d(0, 0);
        _tiles = new char[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            string line = lines[y].Trim();
            for (int x = 0; x < Width; x++)
            {
                char ch = line[x];
                _tiles[x, y] = ch;
                if (ch == 'S')
                {
                    StartTilePosition = new Point2d(x, y);
                }
                else if (ch == 'E')
                {
                    EndTilePosition = new Point2d(x, y);
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
                sb.Append(_tiles[x, y]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void Print()
    {
        Console.WriteLine(ToString());
    }

    public DirectedGraph GenerateGraph()
    {
        DirectedGraph graph = new();
        Point2d p = StartTilePosition;
        HashSet<(Point2d, Direction)> visitedPositions = [];
        AddNodeToGraph(p, Direction.Right, graph, visitedPositions);
        return graph;
    }

    private void AddNodeToGraph(Point2d p, Direction direction, DirectedGraph graph, HashSet<(Point2d, Direction)> visitedPositions)
    {
        graph.AddVertex(new GraphNode(p, direction));
        visitedPositions.Add((p, direction));
        if (p == EndTilePosition)
        {
            return;
        }
        foreach ((Direction newDirection, Point2d movement) in DirectionHelpers.Movements)
        {
            Point2d newP = p.Move(movement);
            if (DirectionHelpers.Opposite[newDirection] != direction &&
                Contains(newP) &&
                this[newP] != '#')
            {
                if (!visitedPositions.Contains((newP, newDirection)))
                {
                    AddNodeToGraph(newP, newDirection, graph, visitedPositions);
                }
                int cost = newDirection == direction ? 1 : 1001;
                graph.AddEdge(graph.GetAt(p, direction), graph.GetAt(newP, newDirection), cost);
            }
        }
    }

    public (int shortestPath, int nrNodesOnBestPaths) GetShortestPath()
    {
        DirectedGraph graph = GenerateGraph();
        graph.ToHtml(this, "graph.html");
        return graph.ComputeShortestPathFromStartToEnd(StartTilePosition, EndTilePosition);
    }
}
