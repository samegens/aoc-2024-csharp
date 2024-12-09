namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string line = File.ReadAllText("input.txt");
        Solver solver = new(line);
        Console.WriteLine($"Part 1: {solver.SolvePart1()}");
        Console.WriteLine($"Part 2: {solver.SolvePart2()}");
    }
}
