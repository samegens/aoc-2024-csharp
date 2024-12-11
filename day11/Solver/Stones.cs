
namespace AoC;

public class Stones : List<Stone>
{
    private readonly Dictionary<(long, int), long> _numberGenerationToStoneCountMap = [];

    public long TotalStoneCount
    {
        get
        {
            long totalStoneCount = 0;
            foreach (Stone stone in this)
            {
                if (stone.IsPlaceHolder)
                {
                    totalStoneCount += _numberGenerationToStoneCountMap[(stone.Number, stone.Generation)];
                }
                else
                {
                    totalStoneCount++;
                }
            }
            return totalStoneCount;
        }
    }

    public Stones(IEnumerable<Stone> stones) : base(stones) { }

    public static Stones Parse(string input)
    {
        return new Stones(input
                        .Split(' ')
                        .Select(x => new Stone(long.Parse(x), 0)));
    }

    public void Blink(int n)
    {
        PrecomputeNumberGenerations();
        // Stones referenceStones = new(this);
        for (int i = 0; i < n; i++)
        {
            // Console.WriteLine(i);
            Blink(true);
            // referenceStones.Blink(false);
            // if (TotalStoneCount != referenceStones.TotalStoneCount)
            // {
            //     Console.WriteLine($"Mismatch at {i}:");
            //     Print();
            //     referenceStones.Print();
            //     throw new Exception();
            // }
            //SAMTODO
            // Print();
            // foreach (Stone stone in this)
            // {
            //     if (_originalStones.Contains(stone.Number))
            //     {
            //         Console.WriteLine($"Found cycle! ({stone.Number})");
            //     }
            // }
        }
    }

    public void Blink(bool optimize = true)
    {
        List<Stone> oldStones = new(this);
        Clear();
        foreach (Stone stone in oldStones)
        {
            AddRange(stone.Blink());
        }

        if (optimize)
        {
            ReplaceKnownStonesByPlaceholders();
        }
    }

    private void ReplaceKnownStonesByPlaceholders()
    {
        List<Stone> oldStones = new(this);
        Clear();
        foreach (Stone stone in oldStones)
        {
            if (_numberGenerationToStoneCountMap.ContainsKey((stone.Number, stone.Generation)))
            {
                Add(stone.AsPlaceholder());
            }
            else
            {
                Add(stone);
            }
        }
    }

    public void PrecomputeNumberGenerations()
    {
        List<Stones> stonesPerNumber = [];
        for (int number = 0; number < 10; number++)
        {
            _numberGenerationToStoneCountMap[(number, 0)] = 1;
            stonesPerNumber.Add(Parse(number.ToString()));
        }
        for (int generation = 1; generation < 75; generation++)
        {
            for (int number = 0; number < 10; number++)
            {
                Stones nextGenerationStones = new([]);
                stonesPerNumber[number].Blink();
                long totalNrStones = 0;
                foreach (Stone stone in stonesPerNumber[number])
                {
                    if (_numberGenerationToStoneCountMap.ContainsKey((stone.Number, stone.Generation)))
                    {
                        nextGenerationStones.Add(new Stone(stone.Number, stone.Generation, true));
                        totalNrStones += _numberGenerationToStoneCountMap[(stone.Number, stone.Generation)];
                    }
                    else
                    {
                        nextGenerationStones.Add(stone);
                        totalNrStones++;
                    }
                }
                _numberGenerationToStoneCountMap[(number, generation)] = totalNrStones;
                // Console.WriteLine($"{number} {generation}: {nextGenerationStones.Count}/{totalNrStones}");
                // nextGenerationStones.Print();
                stonesPerNumber[number] = nextGenerationStones;
            }
        }
        // Console.WriteLine(NumberGenerationToStoneCountMap);
    }

    private void Print()
    {
        Console.WriteLine(string.Join(',', this.Select(s => s.Number)));
    }
}
