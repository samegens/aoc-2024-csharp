namespace AoC;

public class Solver(List<string> lines)
{
    public string SolvePart1()
    {
        Computer computer = Computer.Parse(lines);
        computer.ExecuteProgram();
        return string.Join(',', computer.Output);
    }

    public long SolvePart2()
    {
        Computer computer = Computer.Parse(lines);
        return computer.FindAThatReplicatesProgram();
    }
}
