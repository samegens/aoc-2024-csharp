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
        long D = ButtonADelta.X * ButtonBDelta.Y - ButtonBDelta.X * ButtonADelta.Y;
        long Dx = PrizePosition.X * ButtonBDelta.Y - PrizePosition.Y * ButtonBDelta.X;
        long Dy = ButtonADelta.X * PrizePosition.Y - ButtonADelta.Y * PrizePosition.X;

        if (Dx % D != 0 || Dy % D != 0)
        {
            return 0;
        }

        long nrAPresses = Dx / D;
        long nrBPresses = Dy / D;

        return 3 * nrAPresses + nrBPresses;
        // long minNrTokens = long.MaxValue;
        // for (long nrAPresses = 1; nrAPresses <= 100; nrAPresses++)
        // {
        //     long prizeX = PrizePosition.X - nrAPresses * ButtonADelta.X;
        //     long prizeY = PrizePosition.Y - nrAPresses * ButtonADelta.Y;
        //     if (prizeX % ButtonBDelta.X == 0 && prizeY % ButtonBDelta.Y == 0)
        //     {
        //         long nrBPresses = prizeX / ButtonBDelta.X;
        //         if (prizeY / ButtonBDelta.Y == nrBPresses)
        //         {
        //             long nrTokens = 3 * nrAPresses + nrBPresses;
        //             minNrTokens = Math.Min(minNrTokens, nrTokens);
        //         }
        //     }
        // }
        // return minNrTokens;
    }
}