namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        Network network = Network.Parse(lines);
        HashSet<string> clusters = network.FindClusters();
        return clusters
            .Count(c => c[0] == 't' || c[3] == 't' || c[6] == 't');
    }

    public string SolvePart2()
    {
        Network network = Network.Parse(lines);
        List<GraphNode> nodes = network.GetLanParty();
        return string.Join(',', nodes
            .Select(n => n.Name)
            .Order());
    }
}
