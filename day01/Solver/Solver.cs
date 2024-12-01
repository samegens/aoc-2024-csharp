namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        (List<int> leftNumbers, List<int> rightNumbers) = ParseLines(lines);
        leftNumbers.Sort();
        rightNumbers.Sort();

        int sum = 0;
        for (int i = 0; i < leftNumbers.Count; i++)
        {
            sum += Math.Abs(leftNumbers[i] - rightNumbers[i]);
        }

        return sum;
    }

    public int SolvePart2()
    {
        (List<int> leftNumbers, List<int> rightNumbers) = ParseLines(lines);
        int sum = 0;
        for (int i = 0; i < leftNumbers.Count; i++)
        {
            int number = leftNumbers[i];
            int count = rightNumbers.Count(n => n == number);
            sum += number * count;
        }

        return sum;
    }

    public static (List<int> leftNumbers, List<int> rightNumbers) ParseLines(List<string> lines)
    {
        List<int> leftNumbers = [];
        List<int> rightNumbers = [];
        foreach (string line in lines)
        {
            (int leftNumber, int rightNumber) = ParseLine(line);
            leftNumbers.Add(leftNumber);
            rightNumbers.Add(rightNumber);
        }

        return (leftNumbers, rightNumbers);
    }

    public static (int leftNumber, int rightNumber) ParseLine(string line)
    {
        List<string> parts = line.Trim()
            .Replace("  ", " ")
            .Split(' ')
            .Where(s => !string.IsNullOrEmpty(s))
            .ToList();
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
}
