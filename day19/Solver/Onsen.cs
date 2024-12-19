
namespace AoC;

public class Onsen()
{
    public List<string> Towels { get; private set; } = [];
    public List<string> Designs { get; private set; } = [];
    // The search space is small for the example, but huge for the actual input.
    // We can greatly reduce it by caching previous results.
    // (Memoization, see https://en.wikipedia.org/wiki/Memoization)
    private Dictionary<string, bool> _cachedIsPossible = [];
    private Dictionary<string, long> _cachedNrWays = [];

    public static Onsen Parse(List<string> lines)
    {
        return new Onsen()
        {
            Towels = lines[0]
                .Split(", ")
                .ToList(),
            Designs = lines
                .Skip(2)
                .ToList()
        };
    }

    public bool IsDesignPossible(string design)
    {
        if (_cachedIsPossible.ContainsKey(design))
        {
            return _cachedIsPossible[design];
        }

        foreach (string towel in Towels)
        {
            if (towel == design)
            {
                _cachedIsPossible[design] = true;
                return true;
            }
            if (design.StartsWith(towel))
            {
                if (IsDesignPossible(design[towel.Length..]))
                {
                    return true;
                }
            }
        }

        _cachedIsPossible[design] = false;
        return false;
    }

    public long GetNrWaysToCreateDesign(string design)
    {
        if (_cachedNrWays.ContainsKey(design))
        {
            return _cachedNrWays[design];
        }

        long nrWays = 0;
        foreach (string towel in Towels)
        {
            if (towel == design)
            {
                nrWays += 1;
            }
            else if (design.StartsWith(towel))
            {
                nrWays += GetNrWaysToCreateDesign(design[towel.Length..]);
            }
        }

        _cachedNrWays[design] = nrWays;
        return nrWays;

    }
}