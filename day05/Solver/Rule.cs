namespace AoC;

public class Rule
{
    public int First { get; set; }
    public int Later { get; set; }

    public Rule(int first, int later)
    {
        First = first;
        Later = later;
    }

    public Rule(Rule rule)
    {
        First = rule.First;
        Later = rule.Later;
    }

    public static Rule Parse(string line)
    {
        string[] parts = line.Split('|');
        return new Rule(int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public override string ToString()
    {
        return $"({First},{Later})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Rule otherRule)
        {
            return First == otherRule.First && Later == otherRule.Later;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Later);
    }

    internal void Print()
    {
        Console.WriteLine($"{First},{Later}");
    }
}