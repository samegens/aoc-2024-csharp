
namespace AoC;

public class Gate(string _wireIn1, string _wireIn2, string _op, string _wireOut)
{
    public string WireIn1 => _wireIn1;
    public string WireIn2 => _wireIn2;
    public string Op => _op;
    public string WireOut => _wireOut;

    public string Name => $"{WireIn1}{Op}{WireIn2}";

    public static Gate Parse(string line)
    {
        line = line.Replace(" OR", " OR ");
        string wireIn1 = line[0..3];
        string wireIn2 = line[8..11];
        string op = line[4..7].Trim();
        string wireOut = line[15..18];
        Gate gate = new(wireIn1, wireIn2, op, wireOut);
        return gate;
    }

    public int Execute(int v1, int v2)
    {
        switch (Op)
        {
            case "AND":
                return v1 & v2;
            case "OR":
                return v1 | v2;
            case "XOR":
                return v1 ^ v2;
            default:
                throw new Exception($"Unknown operation {Op}");
        }
    }

    public bool HasWireIn(string name) => WireIn1 == name || WireIn2 == name;
}