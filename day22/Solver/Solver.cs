

namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        return lines
            .Select(long.Parse)
            .Sum(Generate2000thSecret);
    }

    private static long Generate2000thSecret(long seed)
    {
        Secret secret = new(seed);
        for (int i = 0; i < 2000; i++)
        {
            secret.GenerateNext();
        }
        return secret.Current;
    }

    public long SolvePart2()
    {
        List<Buyer> buyers = lines
            .Select(l => new Buyer(long.Parse(l)))
            .ToList();
        HashSet<string> allSequences = new(buyers.SelectMany(b => b.GetSequences()));
        return allSequences
            .Max(s => GetTotalBananasForSequence(s, buyers));
    }

    private static long GetTotalBananasForSequence(string sequence, List<Buyer> buyers)
    {
        return buyers.Sum(b => b.GetSequencePrice(sequence));
    }
}
