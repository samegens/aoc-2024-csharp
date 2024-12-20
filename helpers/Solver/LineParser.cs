using System.Text.RegularExpressions;

namespace AoC;

public static class LineParser
{
    public static (int i1, int i2) Parse2Ints(string input)
    {
        int i1 = 0;
        int i2 = 0;

        Match match = Regex.Match(input, @"(-?\d+)[^0-9-]+(-?\d+)");
        if (match.Success)
        {
            i1 = int.Parse(match.Groups[1].Value);
            i2 = int.Parse(match.Groups[2].Value);
        }

        return (i1, i2);
    }

    public static (int actual1, int actual2, int actual3) Parse3Ints(string input)
    {
        int i1 = 0;
        int i2 = 0;
        int i3 = 0;

        Match match = Regex.Match(input, @"(-?\d+)[^0-9-]+(-?\d+)[^0-9-]+(-?\d+)");
        if (match.Success)
        {
            i1 = int.Parse(match.Groups[1].Value);
            i2 = int.Parse(match.Groups[2].Value);
            i3 = int.Parse(match.Groups[3].Value);
        }

        return (i1, i2, i3);
    }

    public static (int actual1, int actual2, int actual3, int actual4) Parse4Ints(string input)
    {
        int i1 = 0;
        int i2 = 0;
        int i3 = 0;
        int i4 = 0;

        Match match = Regex.Match(input, @"(-?\d+)[^0-9-]+(-?\d+)[^0-9-]+(-?\d+)[^0-9-]+(-?\d+)");
        if (match.Success)
        {
            i1 = int.Parse(match.Groups[1].Value);
            i2 = int.Parse(match.Groups[2].Value);
            i3 = int.Parse(match.Groups[3].Value);
            i4 = int.Parse(match.Groups[4].Value);
        }

        return (i1, i2, i3, i4);
    }
}