namespace AoC;

public class Solver(string line)
{
    public long SolvePart1()
    {
        FileSystem fileSystem = FileSystem.Parse(line);
        fileSystem.Defragment();
        return fileSystem.ComputeChecksum();
    }

    public long SolvePart2()
    {
        FileSystem fileSystem = FileSystem.Parse(line);
        fileSystem.DefragmentWholeBlocks();
        return fileSystem.ComputeChecksum();
    }
}
