namespace AoC;

public static class Algorithms
{
    // Adopted from https://stackoverflow.com/a/29717490
    public static long LCM(IEnumerable<long> numbers)
    {
        return numbers.Aggregate(LCM);
    }

    public static long LCM(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

    public static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

    /// <summary>
    /// Solve linear equations of the form:
    /// a1 * x + b1 * y = c1
    /// a2 * x + b2 * y = c2
    /// </summary>
    public static (bool hasSolution, long x, long y) SolveLinearEquations2x2(long a1, long b1, long c1, long a2, long b2, long c2)
    {
        // From https://byjus.com/maths/cramers-rule/
        long D = a1 * b2 - a2 * b1;
        long Dx = c1 * b2 - c2 * b1;
        long Dy = a1 * c2 - a2 * c1;

        if (Dx % D != 0 || Dy % D != 0)
        {
            return (false, 0, 0);
        }

        long x = Dx / D;
        long y = Dy / D;

        return (true, x, y);
    }
}