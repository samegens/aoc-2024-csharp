namespace AoC;

public class Stones
{
    private Dictionary<long, long> _frequencyMap = [];

    public long TotalStoneCount => _frequencyMap.Values.Sum();

    public Stones(IEnumerable<long> stones)
    {
        foreach (long stone in stones)
        {
            IncreaseFrequencyCount(_frequencyMap, stone, 1);
        }
    }

    public static Stones Parse(string input)
    {
        return new(input
                        .Split(' ')
                        .Select(long.Parse));
    }

    public void Blink(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Blink();
        }
    }

    public void Blink()
    {
        Dictionary<long, long> newFrequencyMap = [];
        foreach ((long number, long frequency) in _frequencyMap)
        {
            int numberNrDigits = $"{number}".Length;
            if (number == 0)
            {
                IncreaseFrequencyCount(newFrequencyMap, 1, frequency);
            }
            else if (numberNrDigits % 2 == 0)
            {
                long firstNumber = long.Parse($"{number}"[..(numberNrDigits / 2)]);
                long secondNumber = long.Parse($"{number}"[(numberNrDigits / 2)..]);
                IncreaseFrequencyCount(newFrequencyMap, firstNumber, frequency);
                IncreaseFrequencyCount(newFrequencyMap, secondNumber, frequency);
            }
            else
            {
                IncreaseFrequencyCount(newFrequencyMap, number * 2024, frequency);
            }
        }
        _frequencyMap = newFrequencyMap;
    }

    private static void IncreaseFrequencyCount(Dictionary<long, long> frequencyMap, long number, long delta)
    {
        if (!frequencyMap.ContainsKey(number))
        {
            frequencyMap[number] = delta;
        }
        else
        {
            frequencyMap[number] = frequencyMap[number] + delta;
        }
    }
}
