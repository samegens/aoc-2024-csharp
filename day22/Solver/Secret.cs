namespace AoC;

public class Secret
{
    public long Current { get; private set; }

    public Secret(long seed)
    {
        Current = seed;
    }

    public void GenerateNext()
    {
        Current = (Current ^ (Current * 64)) % 16777216;
        Current = (Current ^ (Current / 32)) % 16777216;
        Current = (Current ^ (Current * 2048)) % 16777216;
    }
}