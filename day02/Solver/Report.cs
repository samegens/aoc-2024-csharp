namespace AoC;

public class Report(List<int> levels)
{
    public static Report Parse(string line)
    {
        return new Report(line.Trim()
                              .Split(' ')
                              .Select(t => int.Parse(t))
                              .ToList());
    }

    public List<int> Levels => levels;

    public bool IsSafe
    {
        get
        {
            int expectedSign = Math.Sign(levels[1] - levels[0]);
            for (int i = 0; i < levels.Count - 1; i++)
            {
                int diff = Math.Abs(levels[i + 1] - levels[i]);
                if (diff < 1 || diff > 3)
                {
                    return false;
                }
                int sign = Math.Sign(levels[i + 1] - levels[i]);
                if (sign != expectedSign)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public bool IsSafeWithProblemDampener
    {
        get
        {
            if (IsSafe)
            {
                return true;
            }

            for (int i = 0; i < levels.Count; i++)
            {
                List<int> levelsMinusOne = new(levels);
                levelsMinusOne.RemoveAt(i);
                Report alternativeReport = new(levelsMinusOne);
                if (alternativeReport.IsSafe)
                {
                    return true;
                }
            }

            return false;
        }
    }
}