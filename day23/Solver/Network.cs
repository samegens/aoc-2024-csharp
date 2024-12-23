


namespace AoC;

public class Network
{
    private readonly NonDirectedGraph _graph;

    public Network(NonDirectedGraph graph)
    {
        _graph = graph;
    }

    public int VertexCount => _graph.Count;

    public static Network Parse(List<string> lines)
    {
        NonDirectedGraph graph = new();
        foreach (string line in lines)
        {
            (string name1, string name2) = ParseLine(line);
            GraphNode node1 = graph.AddOrCreateNode(name1);
            GraphNode node2 = graph.AddOrCreateNode(name2);
            graph.AddEdge(node1, node2, 1);
        }
        return new Network(graph);
    }

    private static (string node1, string node2) ParseLine(string line)
    {
        string[] parts = line.Trim().Split('-');
        return (parts[0], parts[1]);
    }

    public HashSet<HashSet<string>> FindClusters()
    {
        return _graph.FindClusters();
    }

    public List<GraphNode> GetLanParty()
    {
        return _graph.GetLargestCluster();
    }
}