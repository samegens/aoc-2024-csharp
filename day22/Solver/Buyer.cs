namespace AoC;

public class Buyer
{
    private readonly List<int> prices = [];
    private readonly List<int> changes = [];
    private readonly Dictionary<string, int> sequencePrice = [];

    public Buyer(long seed)
    {
        Secret secret = new(seed);
        for (int i = 0; i < 2000; i++)
        {
            secret.GenerateNext();
            int price = (int)(secret.Current % 10);
            prices.Add(price);
            if (i > 0)
            {
                changes.Add(prices[i] - prices[i - 1]);
            }
            if (i > 4)
            {
                string sequence = GetSequenceFromChanges(changes[i - 4], changes[i - 3], changes[i - 2], changes[i - 1]);

                // The monkey will react to the first occurrance of the sequence, so
                // we only store the first one.
                if (!sequencePrice.ContainsKey(sequence))
                {
                    sequencePrice[sequence] = price;
                }
            }
        }
    }

    public int GetSequencePrice(string sequence)
    {
        if (sequencePrice.TryGetValue(sequence, out int price))
        {
            return price;
        }

        return 0;
    }

    public List<string> GetSequences() => sequencePrice.Keys.ToList();

    public static string GetSequenceFromChanges(int d1, int d2, int d3, int d4)
    {
        char c1 = (char)d1;
        char c2 = (char)d2;
        char c3 = (char)d3;
        char c4 = (char)d4;
        return $"{c1}{c2}{c3}{c4}";
    }

    public static (int d1, int d2, int d3, int d4) GetChangesFromSequence(string sequence)
    {
        return ((short)sequence[0], (short)sequence[1], (short)sequence[2], (short)sequence[3]);
    }

    public static string GetBestSequence(List<Buyer> buyers)
    {
        HashSet<string> allSequences = new(buyers.SelectMany(b => b.GetSequences()));
        string bestSequence = "";
        long bestNrBananas = 0;
        foreach (string sequence in allSequences)
        {
            long nrBananas = buyers.Sum(b => b.GetSequencePrice(sequence));
            if (nrBananas > bestNrBananas)
            {
                bestSequence = sequence;
                bestNrBananas = nrBananas;
            }
        }
        return bestSequence;
    }
}
