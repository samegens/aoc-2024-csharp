






namespace AoC;

public class Solver(List<string> lines)
{
    public (List<Rule> rules, List<List<int>> updates) Parse()
    {
        int separatorIndex = FindSeparator(lines);
        List<Rule> rules = ParseRules(lines[0..separatorIndex]);
        List<List<int>> updates = ParseUpdates(lines[(separatorIndex + 1)..]);

        return (rules, updates);
    }

    public static List<Rule> ParseRules(List<string> list)
    {
        return list.Select(l => Rule.Parse(l)).ToList();
    }

    private static int FindSeparator(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            if (string.IsNullOrEmpty(lines[i].Trim()))
            {
                return i;
            }
        }

        throw new InvalidDataException("No separator found");
    }

    public static List<List<int>> ParseUpdates(List<string> list) => list.Select(ParseUpdate).ToList();

    public static List<int> ParseUpdate(string l) => l.Split(',').Select(int.Parse).ToList();

    public int SolvePart1()
    {
        (List<Rule> rules, List<List<int>> updates) = Parse();
        return updates
            .Where(u => IsInCorrectOrder(u, rules))
            .Sum(GetMiddlePageNr);
    }

    public int SolvePart2()
    {
        (List<Rule> rules, List<List<int>> updates) = Parse();
        return updates
            .Where(u => !IsInCorrectOrder(u, rules))
            .Select(u => OrderUpdate(u, rules))
            .Sum(GetMiddlePageNr);
    }

    public static bool IsInCorrectOrder(List<int> update, List<Rule> rules)
    {
        foreach (Rule rule in rules)
        {
            int firstPos = update.IndexOf(rule.First);
            int laterPos = update.IndexOf(rule.Later);
            if (firstPos >= 0 && laterPos >= 0)
            {
                if (laterPos <= firstPos)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static int GetMiddlePageNr(List<int> update)
    {
        int middleIndex = update.Count / 2;
        return update[middleIndex];
    }

    public static List<int> OrderUpdate(List<int> update, List<Rule> rules)
    {
        List<int> orderedUpdate = new(update);

        bool isDone;
        do
        {
            isDone = true;
            foreach (Rule rule in rules)
            {
                int firstPos = orderedUpdate.IndexOf(rule.First);
                int laterPos = orderedUpdate.IndexOf(rule.Later);
                if (firstPos >= 0 && laterPos >= 0 && laterPos < firstPos)
                {
                    // Incorrect order, we need to fix that.
                    (orderedUpdate[firstPos], orderedUpdate[laterPos]) = (orderedUpdate[laterPos], orderedUpdate[firstPos]);
                    isDone = false;
                }
            }
        }
        while (!isDone);

        return orderedUpdate;
    }
}
