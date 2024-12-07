

namespace AoC;

public class Equation(long testValue, List<long> numbers)
{
    public long TestValue => testValue;
    public List<long> Numbers => numbers;

    public override string ToString() => $"{TestValue}: {string.Join(' ', Numbers)}";

    public static Equation Parse(string line)
    {
        long testValue = long.Parse(line.Split(':')[0]);
        List<long> numbers = line.Split(':')[1]
            .Trim()
            .Split(' ')
            .Select(long.Parse)
            .ToList();
        return new Equation(testValue, numbers);
    }

    public override bool Equals(object? otherObj)
    {
        if (otherObj == null || GetType() != otherObj.GetType())
        {
            return false;
        }

        Equation otherEquation = (Equation)otherObj;
        return otherEquation.TestValue == TestValue &&
            otherEquation.Numbers.SequenceEqual(Numbers);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TestValue, Numbers);
    }

    public bool CanBeSolved()
    {
        return CanBeSolved(0, 0);
    }

    private bool CanBeSolved(int numberIndex, long total)
    {
        if (numberIndex == Numbers.Count)
        {
            return total == TestValue;
        }

        if (total > TestValue)
        {
            return false;
        }

        if (CanBeSolved(numberIndex + 1, total + Numbers[numberIndex]))
        {
            return true;
        }
        return CanBeSolved(numberIndex + 1, total * Numbers[numberIndex]);
    }

    public bool CanBeSolvedWithConcatenation()
    {
        return CanBeSolvedWithConcatenation(0, 0);
    }

    private bool CanBeSolvedWithConcatenation(int numberIndex, long total)
    {
        if (numberIndex == Numbers.Count)
        {
            return total == TestValue;
        }

        if (total > TestValue)
        {
            return false;
        }

        if (CanBeSolvedWithConcatenation(numberIndex + 1, total + Numbers[numberIndex]))
        {
            return true;
        }
        if (CanBeSolvedWithConcatenation(numberIndex + 1, total * Numbers[numberIndex]))
        {
            return true;
        }
        return CanBeSolvedWithConcatenation(numberIndex + 1, long.Parse($"{total}{Numbers[numberIndex]}"));
    }
}
