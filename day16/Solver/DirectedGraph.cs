using System.Text;

namespace AoC;

public class GraphNode
{
    public GraphNode(Point2d pos, Direction direction)
    {
        Pos = pos;
        Direction = direction;
    }

    public Point2d Pos { get; set; }
    public Direction Direction { get; private set; }

    public override string ToString()
    {
        return $"{Pos}|{Direction}";
    }

    public int ShortestPath { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        GraphNode other = (GraphNode)obj;

        return other.Pos.Equals(Pos) && other.Direction == Direction;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Pos, Direction);
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

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Edge other = (Edge)obj;

        return First.Equals(other.First) &&
               Second.Equals(other.Second) &&
               Cost == other.Cost;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second, Cost);
    }

    public override string ToString()
    {
        return $"{First}-{Second} ({Cost})";
    }
}

public class DirectedGraph
{
    private readonly Dictionary<GraphNode, List<Edge>> _adjacencyList = [];
    // Dictionary to speed up position lookups
    private readonly Dictionary<(Point2d, Direction), GraphNode> _pointDirectionToNodeMap = [];

    public DirectedGraph()
    {
    }

    public int Count => _pointDirectionToNodeMap.Count;

    public void AddVertex(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = new List<Edge>();
            _pointDirectionToNodeMap[(vertex.Pos, vertex.Direction)] = vertex;
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

        if (!_pointDirectionToNodeMap[(vertex1.Pos, vertex1.Direction)].Equals(vertex1))
        {
            throw new Exception($"Vertex1 {vertex1} has a mismatched reference in the graph!");
        }

        if (!_pointDirectionToNodeMap[(vertex2.Pos, vertex2.Direction)].Equals(vertex2))
        {
            throw new Exception($"Vertex2 {vertex2} has a mismatched reference in the graph!");
        }

        Edge edge = new(vertex1, vertex2, cost);
        if (!_adjacencyList[vertex1].Contains(edge))
        {
            _adjacencyList[vertex1].Add(edge);
        }
    }

    public List<Edge> GetEdges(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            return new List<Edge>();
        }

        return _adjacencyList[vertex];
    }

    public List<Edge> GetEdgesTo(Point2d p)
    {
        List<Edge> edges = [];

        foreach (Direction direction in DirectionHelpers.Directions)
        {
            if (Contains(p, direction))
            {
                GraphNode thisNode = GetAt(p, direction);
                Point2d neighbourPos = p.MoveOpposite(direction);
                List<GraphNode> neighbourNodes = _adjacencyList.Keys
                    .Where(graphNode => graphNode.Pos == neighbourPos)
                    .ToList();
                List<Edge> allNeighbourEdges = neighbourNodes
                    .SelectMany(graphnode => _adjacencyList[graphnode])
                    .ToList();
                List<Edge> neighbourEdgesToThisNode = allNeighbourEdges
                    .Where(edge => edge.Second == thisNode)
                    .ToList();
                edges.AddRange(neighbourEdgesToThisNode);
            }
        }

        return edges;
    }

    public List<Edge> GetEdgesTo(GraphNode node)
    {
        Point2d neighbourPos = node.Pos.MoveOpposite(node.Direction);
        List<GraphNode> neighbourNodes = _adjacencyList.Keys
            .Where(graphNode => graphNode.Pos == neighbourPos)
            .ToList();
        List<Edge> allNeighbourEdges = neighbourNodes
            .SelectMany(graphnode => _adjacencyList[graphnode])
            .ToList();
        return allNeighbourEdges
            .Where(edge => edge.Second == node)
            .ToList();
    }

    public List<GraphNode> GetNeighbors(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            return new List<GraphNode>();
        }

        return _adjacencyList[vertex].Select(e => e.Second).ToList();
    }

    public bool Contains(Point2d pos, Direction direction) => _pointDirectionToNodeMap.ContainsKey((pos, direction));

    public GraphNode GetAt(Point2d pos, Direction direction)
    {
        return _pointDirectionToNodeMap[(pos, direction)];
    }

    public void Print()
    {
        foreach (var kvp in _adjacencyList)
        {
            Console.Write($"{kvp.Key} -> ");
            Console.WriteLine(string.Join(", ", kvp.Value));
        }
    }

    public (int shortestPath, int nrNodesOnBestPaths) ComputeShortestPathFromStartToEnd(Point2d start, Point2d end)
    {
        var queue = new PriorityQueue<GraphNode, int>();
        var visited = new HashSet<GraphNode>();
        var shortestPaths = new Dictionary<GraphNode, int>();
        foreach (var kvp in _adjacencyList)
        {
            shortestPaths[kvp.Key] = int.MaxValue;
        }

        shortestPaths[GetAt(start, Direction.Right)] = 0;
        queue.Enqueue(GetAt(start, Direction.Right), 0);
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

        int shortestPath = DirectionHelpers.Directions
            .Where(d => Contains(end, d))
            .Min(d => shortestPaths[GetAt(end, d)]);
        IEnumerable<GraphNode> endNodes = DirectionHelpers.Directions
            .Where(d => Contains(end, d))
            .Select(d => GetAt(end, d))
            .Where(x => shortestPaths[x] == shortestPath);
        HashSet<GraphNode> nodesOnShortestPaths = [];

        foreach (GraphNode node in endNodes)
        {
            FindShortestPathNodes(node, nodesOnShortestPaths, shortestPaths);
        }
        HashSet<Point2d> tilesOnBestPath = new(nodesOnShortestPaths.Select(n => n.Pos));
        int nrNodesOnBestPaths = tilesOnBestPath.Count;
        return (shortestPath, nrNodesOnBestPaths);
    }

    private void FindShortestPathNodes(GraphNode currentNode, HashSet<GraphNode> nodesOnShortestPaths, Dictionary<GraphNode, int> shortestPaths)
    {
        nodesOnShortestPaths.Add(currentNode);
        List<Edge> edgesToCurrentNode = GetEdgesTo(currentNode);
        if (edgesToCurrentNode.Count != 0)
        {
            List<GraphNode> sourceNodes = edgesToCurrentNode.Select(e => e.First).ToList();
            int shortestPath = edgesToCurrentNode.Min(e => shortestPaths[e.First] + e.Cost);
            IEnumerable<GraphNode> sourceNodesOnShortestPath = edgesToCurrentNode
                .Where(e => shortestPaths[e.First] + e.Cost == shortestPath)
                .Select(e => e.First);
            foreach (GraphNode sourceNodeOnShortestPath in sourceNodesOnShortestPath)
            {
                FindShortestPathNodes(sourceNodeOnShortestPath, nodesOnShortestPaths, shortestPaths);
            }
        }
    }

    public void ToHtml(Board board, string outputPath)
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
                        ? string.Join(", ", edges.Select(e => $"{e.Second.Direction}={e.Cost}"))
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

        File.WriteAllText(outputPath, html.ToString());
    }
}
