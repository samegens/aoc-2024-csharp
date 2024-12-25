namespace AoC;

public class Solver(List<string> lines)
{
    public long SolvePart1()
    {
        List<List<int>> locks = [];
        List<List<int>> keys = [];
        for (int i = 0; i < lines.Count; i += 8)
        {
            int[] heights = new int[5];
            for (int j = i + 1; j < i + 6; j++)
            {
                for (int pos = 0; pos < 5; pos++)
                {
                    if (lines[j][pos] == '#')
                    {
                        heights[pos]++;
                    }
                }
            }
            if (lines[i][0] == '#')
            {
                locks.Add(heights.ToList());
            }
            else
            {
                keys.Add(heights.ToList());
            }
        }

        long nrMatches = 0;
        foreach (List<int> locky in locks)
        {
            foreach (List<int> key in keys)
            {
                bool hasMatch = true;
                for (int pos = 0; pos < 5; pos++)
                {
                    if (key[pos] + locky[pos] > 5)
                    {
                        hasMatch = false;
                    }
                }
                if (hasMatch)
                {
                    nrMatches++;
                }
            }
        }
        return nrMatches;
    }

    public int SolvePart2()
    {
        return 0;
    }
}
