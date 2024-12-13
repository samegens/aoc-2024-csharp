using System.Text.RegularExpressions;

namespace AoC;

public record class Machine(Point2dL ButtonADelta, Point2dL ButtonBDelta, Point2dL PrizePosition)
{
    public static Machine Parse(List<string> input)
    {
        Match match = Regex.Match(input[0], @".*X\+(\d+), Y\+(\d+)");
        long buttonAX = long.Parse(match.Groups[1].Value);
        long buttonAY = long.Parse(match.Groups[2].Value);
        match = Regex.Match(input[1], @".*X\+(\d+), Y\+(\d+)");
        long buttonBX = long.Parse(match.Groups[1].Value);
        long buttonBY = long.Parse(match.Groups[2].Value);
        match = Regex.Match(input[2], @".*X=(\d+), Y=(\d+)");
        long prizeX = long.Parse(match.Groups[1].Value);
        long prizeY = long.Parse(match.Groups[2].Value);
        return new(new(buttonAX, buttonAY), new(buttonBX, buttonBY), new(prizeX, prizeY));
    }

    public static List<Machine> ParseMultiple(List<string> input)
    {
        List<Machine> machines = [];
        for (int i = 0; i < input.Count; i += 4)
        {
            Machine machine = Parse(input[i..(i + 3)]);
            // Console.WriteLine($"{Algorithms.GCD()}")
            machines.Add(machine);
        }
        return machines;
    }

    public long Play()
    {
        (bool hasSolution, long nrAPresses, long nrBPresses) = Algorithms.SolveLinearEquations2x2(
            ButtonADelta.X, ButtonBDelta.X, PrizePosition.X,
            ButtonADelta.Y, ButtonBDelta.Y, PrizePosition.Y);

        if (!hasSolution)
        {
            return 0;
        }

        return 3 * nrAPresses + nrBPresses;
    }
}