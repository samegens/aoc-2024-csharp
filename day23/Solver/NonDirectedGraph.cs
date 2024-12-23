namespace AoC;

public class GraphNode
{
    public GraphNode(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }

    public int ShortestPath { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        GraphNode other = (GraphNode)obj;

        return other.Name.Equals(Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
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
        return $"{First.Name}-{Second.Name} ({Cost})";
    }
}

public class NonDirectedGraph
{
    private readonly Dictionary<GraphNode, List<Edge>> _adjacencyList = new();
    private readonly Dictionary<string, GraphNode> _nameToNodeMap = new();

    public NonDirectedGraph()
    {
    }

    public int Count => _nameToNodeMap.Count;

    public void AddNode(GraphNode node)
    {
        if (!_adjacencyList.ContainsKey(node))
        {
            _adjacencyList[node] = [];
            _nameToNodeMap[node.Name] = node;
        }
    }

    public void AddEdge(GraphNode node1, GraphNode node2, int cost)
    {
        if (!_adjacencyList.ContainsKey(node1))
        {
            throw new Exception($"Node {node1} not found");
        }

        if (!_adjacencyList.ContainsKey(node2))
        {
            throw new Exception($"Node {node2} not found");
        }

        _adjacencyList[node1].Add(new Edge(node1, node2, cost));
        _adjacencyList[node2].Add(new Edge(node2, node1, cost));
    }

    public List<Edge> GetEdges(GraphNode node)
    {
        if (!_adjacencyList.ContainsKey(node))
        {
            return new List<Edge>();
        }

        return _adjacencyList[node];
    }

    public List<Edge> GetEdgesTo(string name) => GetEdges(_nameToNodeMap[name]);

    public List<GraphNode> GetNeighbors(GraphNode node)
    {
        if (!_adjacencyList.ContainsKey(node))
        {
            return new List<GraphNode>();
        }

        return _adjacencyList[node].Select(e => e.Second).ToList();
    }

    public bool Contains(string name) => _nameToNodeMap.ContainsKey(name);

    public GraphNode GetAt(string name)
    {
        return _nameToNodeMap[name];
    }

    public void Print()
    {
        foreach (var kvp in _adjacencyList)
        {
            Console.Write($"{kvp.Key} -> ");
            Console.WriteLine(string.Join(", ", kvp.Value));
        }
    }

    public int ComputeShortestPathFromStartToEnd(string start, string end)
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

    public GraphNode AddOrCreateNode(string name)
    {
        if (_nameToNodeMap.TryGetValue(name, out GraphNode? node))
        {
            return node;
        }

        node = new(name);
        AddNode(node);
        return node;
    }

    public HashSet<HashSet<string>> FindClusters()
    {
        HashSet<HashSet<string>> resultClusters = [];
        foreach (string name in _nameToNodeMap.Keys)
        {
            HashSet<HashSet<string>> nodeClusters = FindClusters(name);
            foreach (HashSet<string> cluster in nodeClusters)
            {
                if (!resultClusters.Any(c => c.SetEquals(cluster)))
                {
                    resultClusters.Add(cluster);
                }
            }
        }
        return resultClusters;
    }

    private HashSet<HashSet<string>> FindClusters(string name)
    {
        GraphNode start = _nameToNodeMap[name];
        Stack<GraphNode> currentPath = [];
        HashSet<HashSet<string>> clusters = [];
        FindPathTo(start, 3, start, currentPath, clusters);
        return clusters;
    }

    private void FindPathTo(GraphNode destNode, int nrSteps, GraphNode currentNode,
                            Stack<GraphNode> currentPath, HashSet<HashSet<string>> clusters)
    {
        if (nrSteps == 0)
        {
            if (currentNode == destNode)
            {
                AddPathToCluster(currentPath, clusters);
            }
            return;
        }

        currentPath.Push(currentNode);
        foreach (GraphNode nextNode in GetNeighbors(currentNode))
        {
            FindPathTo(destNode, nrSteps - 1, nextNode, currentPath, clusters);
        }
        currentPath.Pop();
    }

    private static void AddPathToCluster(Stack<GraphNode> path, HashSet<HashSet<string>> clusters)
    {
        HashSet<string> newCluster = new(path.Select(n => n.Name));
        if (!clusters.Any(c => c.SetEquals(newCluster)))
        {
            clusters.Add(newCluster);
        }
    }

    public List<GraphNode> GetLargestCluster()
    {
        Dictionary<GraphNode, int> neighbourCount = [];
        foreach (GraphNode node in _nameToNodeMap.Values)
        {
            neighbourCount[node] = GetNeighbors(node).Count;
        }

        GraphNode nodeWithMostNeighbours = neighbourCount
            .OrderByDescending(kvp => kvp.Value)
            .First()
            .Key;

        List<GraphNode> cluster = GetNeighbors(nodeWithMostNeighbours);
        cluster.Add(nodeWithMostNeighbours);
        return cluster;
    }
}
