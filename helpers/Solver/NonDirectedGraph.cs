using System.Text;

namespace AoC;

public class GraphNode
{
    public GraphNode(Point2d pos)
    {
        Pos = pos;
    }

    public Point2d Pos { get; set; }

    public override string ToString()
    {
        return $"{Pos}";
    }

    public int ShortestPath { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        GraphNode other = (GraphNode)obj;

        return other.Pos.Equals(Pos);
    }

    public override int GetHashCode()
    {
        return Pos.GetHashCode();
    }
}

public class Edge
{
    public GraphNode First { get; private set; }
    public GraphNode Second { get; private set; }
    public int Cost { get; private set; }

    public Edge(GraphNode first, GraphNode second, int cost)
    {
        if (first.Equals(second))
        {
            throw new Exception("second should be different");
        }

        First = first;
        Second = second;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"{First.Pos}-{Second.Pos} ({Cost})";
    }
}

public class NonDirectedGraph
{
    private readonly Dictionary<GraphNode, List<Edge>> _adjacencyList = new();
    // Dictionary to speed up position lookups
    private readonly Dictionary<Point2d, GraphNode> _pointToNodeMap = new();

    public NonDirectedGraph()
    {
    }

    public int Count => _pointToNodeMap.Count;

    public void AddVertex(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = new List<Edge>();
            _pointToNodeMap[vertex.Pos] = vertex;
        }
    }

    public void AddEdge(GraphNode vertex1, GraphNode vertex2, int cost)
    {
        if (!_adjacencyList.ContainsKey(vertex1))
        {
            throw new Exception($"Vertex {vertex1} not found");
        }

        if (!_adjacencyList.ContainsKey(vertex2))
        {
            throw new Exception($"Vertex {vertex2} not found");
        }

        _adjacencyList[vertex1].Add(new Edge(vertex1, vertex2, cost));
        _adjacencyList[vertex2].Add(new Edge(vertex2, vertex1, cost));
    }

    public List<Edge> GetEdges(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            return new List<Edge>();
        }

        return _adjacencyList[vertex];
    }

    public List<Edge> GetEdgesTo(Point2d p) => GetEdges(_pointToNodeMap[p]);

    public List<GraphNode> GetNeighbors(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            return new List<GraphNode>();
        }

        return _adjacencyList[vertex].Select(e => e.First).ToList();
    }

    public bool Contains(Point2d pos) => _pointToNodeMap.ContainsKey(pos);

    public GraphNode GetAt(Point2d pos)
    {
        return _pointToNodeMap[pos];
    }

    public void Print()
    {
        foreach (var kvp in _adjacencyList)
        {
            Console.Write($"{kvp.Key} -> ");
            Console.WriteLine(string.Join(", ", kvp.Value));
        }
    }

    public int ComputeShortestPathFromStartToEnd(Point2d start, Point2d end)
    {
        GraphNode endNode = GetAt(end);

        var queue = new PriorityQueue<GraphNode, int>();
        var visited = new HashSet<GraphNode>();
        var shortestPaths = new Dictionary<GraphNode, int>();
        foreach (var kvp in _adjacencyList)
        {
            shortestPaths[kvp.Key] = int.MaxValue;
        }

        shortestPaths[GetAt(start)] = 0;
        queue.Enqueue(GetAt(start), 0);
        while (queue.Count > 0)
        {
            GraphNode node = queue.Dequeue();
            if (visited.Contains(node))
            {
                continue;
            }

            visited.Add(node);
            foreach (Edge edge in GetEdges(node))
            {
                GraphNode neighbor = edge.Second;
                int distance = edge.Cost;
                if (shortestPaths[node] + distance < shortestPaths[neighbor])
                {
                    shortestPaths[neighbor] = shortestPaths[node] + distance;
                }
                queue.Enqueue(neighbor, shortestPaths[neighbor]);
            }
        }

        return shortestPaths[endNode];
    }

    public string ToHtml(Board board)
    {
        var html = new StringBuilder();

        html.AppendLine("""
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Graph Visualization</title>
            <style>
                table { border-collapse: collapse; margin-top: 10px; }
                td { width: 25px; height: 25px; text-align: center; border: 1px solid lightgray; cursor: pointer; }
                .wall { background-color: #333; color: white; }
                .clickable { background-color: #ddd; }
                .start { background-color: green; color: white; }
                .end { background-color: red; color: white; }
                .popup {
                    position: absolute;
                    background: #fff;
                    border: 1px solid #aaa;
                    padding: 5px;
                    border-radius: 4px;
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
                    pointer-events: none;
                    font-family: monospace;
                    font-size: 14px;
                    z-index: 1000;
                }
            </style>
        </head>
        <body>
            <h1>Graph Visualization</h1>
            <p>Click on any '.', 'S', or 'E' to view graph node information.</p>
            <table>
        """);

        // Generate the board as an HTML table
        for (int y = 0; y < board.Height; y++)
        {
            html.AppendLine("    <tr>");
            for (int x = 0; x < board.Width; x++)
            {
                char tile = board[x, y];
                string cssClass = tile switch
                {
                    '#' => "wall",
                    'S' => "start clickable",
                    'E' => "end clickable",
                    '.' => "clickable",
                    _ => ""
                };

                html.Append($"        <td class='{cssClass}'");

                if (cssClass.Contains("clickable"))
                {
                    List<Edge> edges = GetEdgesTo(new Point2d(x, y));
                    string nodeInfo = edges.Any()
                        ? string.Join(", ", edges.Select(e => $"{e.Cost}"))
                        : "Node not reachable";
                    html.Append($" onclick=\"showInfo('{x}', '{y}', `{nodeInfo}`, event)\"");
                }

                html.AppendLine($">{tile}</td>");
            }
            html.AppendLine("    </tr>");
        }

        html.AppendLine("""
            </table>
            <script>
                let popup;
                function showInfo(x, y, info, event) {
                    if (popup) { popup.remove(); }
                    popup = document.createElement('div');
                    popup.className = 'popup';
                    popup.innerHTML = 
                        `<strong>Coordinates:</strong> (${x}, ${y})<br>` +
                        `<strong>GraphNode Info:</strong> ${info}`;
                    popup.style.left = `${event.pageX + 10}px`;
                    popup.style.top = `${event.pageY + 10}px`;
                    document.body.appendChild(popup);
                    setTimeout(() => popup.remove(), 3000);
                }
            </script>
        </body>
        </html>
        """);

        return html.ToString();
    }

    public static NonDirectedGraph CreateGraphFromBoard(Board board)
    {
        NonDirectedGraph graph = new();
        HashSet<Point2d> visitedTiles = [];
        AddTileToGraph(board.Start, graph, board, visitedTiles);
        return graph;
    }

    private static void AddTileToGraph(Point2d p, NonDirectedGraph graph, Board board, HashSet<Point2d> visitedTiles)
    {
        GraphNode thisNode = new(p);
        graph.AddVertex(thisNode);
        visitedTiles.Add(p);

        const int movementCost = 1;

        foreach ((_, Point2d delta) in DirectionHelpers.Movements)
        {
            Point2d newP = p.Move(delta);
            if (board.CanBeEntered(newP))
            {
                if (!visitedTiles.Contains(newP))
                {
                    AddTileToGraph(newP, graph, board, visitedTiles);
                }
                GraphNode nextNode = graph.GetAt(newP);
                graph.AddEdge(thisNode, nextNode, movementCost);
            }
        }
    }
}
