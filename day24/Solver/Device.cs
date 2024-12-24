using System.Text;

namespace AoC;

public class Device
{
    private readonly Dictionary<string, int> _wires = [];
    private readonly List<Gate> _gates = [];

    public Device(Dictionary<string, int> wires, List<Gate> gates)
    {
        _wires = wires;
        _gates = gates;
    }


    public int NrGates => _gates.Count;
    public int NrWiresWithValue => _wires.Count;

    public static Device Parse(List<string> input)
    {
        int separatorIndex = input.IndexOf("");
        Dictionary<string, int> wires = ParseWires(input[0..separatorIndex]);
        List<Gate> gates = ParseGates(input.Skip(separatorIndex + 1));
        return new Device(wires, gates);
    }

    private static List<Gate> ParseGates(IEnumerable<string> lines)
    {
        return lines.Select(Gate.Parse).ToList();
    }

    private static Dictionary<string, int> ParseWires(List<string> lines)
    {
        Dictionary<string, int> wires = [];
        foreach (string line in lines)
        {
            string wire = line[0..3];
            string value = line[5..6];
            wires[wire] = int.Parse(value);
        }
        return wires;
    }

    public long ProduceNumber()
    {
        ProcessGates();
        List<string> zWires = _wires.Keys.Where(k => k.StartsWith('z')).Order().ToList();
        List<int> zValues = zWires.Select(w => _wires[w]).ToList();
        long result = 0;
        int bitIndex = 0;
        foreach (int wireValue in zValues)
        {
            result += (long)wireValue << bitIndex;
            bitIndex++;
        }
        return result;
    }

    // I found the rules to detect invalid (swapped) gates by outputting the graph
    // to a dot file and analysing the png.
    public string ToDot()
    {
        StringBuilder sb = new();
        sb.AppendLine("digraph {\n  rankdir=LR;");
        List<Gate> invalidGates = GetInvalidGates();
        foreach (Gate gate in _gates)
        {
            sb.AppendLine($"  {gate.WireIn1} -> {gate.Name};");
            sb.AppendLine($"  {gate.WireIn2} -> {gate.Name};");
            sb.AppendLine($"  {gate.Name} -> {gate.WireOut};");
            string color = gate.Op switch
            {
                "OR" => "lightgreen",
                "AND" => "brown1",
                "XOR" => "gold",
                _ => throw new Exception($"Unknown op {gate.Op}")
            };
            if (invalidGates.Contains(gate))
            {
                sb.AppendLine($"  {gate.Name} [style=filled, fillcolor=black, fontcolor=white];");
            }
            else
            {
                sb.AppendLine($"  {gate.Name} [style=filled, fillcolor={color}];");
            }
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

    private void ProcessGates()
    {
        Queue<Gate> gatesToProcess = new(_gates);
        while (gatesToProcess.Any())
        {
            Gate gate = gatesToProcess.Dequeue();
            if (_wires.ContainsKey(gate.WireIn1) && _wires.ContainsKey(gate.WireIn2))
            {
                _wires[gate.WireOut] = gate.Execute(_wires[gate.WireIn1], _wires[gate.WireIn2]);
            }
            else
            {
                // Try again later when the in-wires have values.
                gatesToProcess.Enqueue(gate);
            }
        }
    }

    private List<Gate> GetConnectedGates(Gate gate)
    {
        return _gates.Where(g => g.HasWireIn(gate.WireOut)).ToList();
    }

    public List<Gate> GetInvalidGates()
    {
        List<Gate> invalidGates = [];
        foreach (Gate gate in _gates)
        {
            // Ugly logic to detect if a gate is invalid. This assumes that all swapped
            // gates are invalid. (correct assumption for the puzzle input)
            if (gate.Op == "XOR")
            {
                if (gate.WireOut[0] != 'z' && gate.WireIn1[0] != 'x' && gate.WireIn1[0] != 'y')
                {
                    invalidGates.Add(gate);
                    continue;
                }
                List<Gate> connectedGates = GetConnectedGates(gate);
                if (connectedGates.Any(g => g.Op == "OR"))
                {
                    invalidGates.Add(gate);
                    continue;
                }
            }
            if (gate.WireOut[0] == 'z' && gate.Op != "XOR" && gate.WireOut != "z45")
            {
                invalidGates.Add(gate);
                continue;
            }
            if (gate.Op == "OR" && ((gate.WireOut[0] == 'z' && gate.WireOut != "z45") ||
                                    gate.WireIn1[0] == 'x' || gate.WireIn1[0] == 'y' ||
                                    gate.WireIn2[0] == 'x' || gate.WireIn2[0] == 'y'))
            {
                invalidGates.Add(gate);
                continue;
            }
            if (gate.Op == "AND" && !gate.HasWireIn("x00"))
            {
                string name = gate.WireOut;
                List<Gate> connectedGates = GetConnectedGates(gate);
                if (connectedGates.Count > 1)
                {
                    invalidGates.Add(gate);
                    continue;
                }
                if (connectedGates.Any(g => g.Op != "OR"))
                {
                    invalidGates.Add(gate);
                    continue;
                }
            }
        }
        return invalidGates;
    }
}