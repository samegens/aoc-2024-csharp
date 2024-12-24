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

    public string ToDot()
    {
        StringBuilder sb = new();
        sb.AppendLine("digraph {\n  rankdir=LR;");
        foreach (Gate gate in _gates)
        {
            sb.AppendLine($"  {gate.WireIn1} -> {gate.WireOut};");
            sb.AppendLine($"  {gate.WireIn2} -> {gate.WireOut};");
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
}