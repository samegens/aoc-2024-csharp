namespace AoC;

public class Computer
{
    public int IP { get; private set; } = 0;
    public long A { get; set; } = 0;
    public long B { get; private set; } = 0;
    public long C { get; private set; } = 0;
    public List<int> Program { get; private set; } = [];
    public List<long> Output { get; private set; } = [];

    public Computer()
    {
    }

    public Computer(Computer other)
    {
        IP = other.IP;
        A = other.A;
        B = other.B;
        C = other.C;
        Program = other.Program;
    }

    public static Computer Parse(List<string> input)
    {
        Computer computer = new();
        computer.A = long.Parse(input[0].Split(':')[1].Trim());
        computer.B = long.Parse(input[1].Split(':')[1].Trim());
        computer.C = long.Parse(input[2].Split(':')[1].Trim());
        computer.Program = input[4].Split(':')[1].Trim().Split(',').Select(int.Parse).ToList();
        return computer;
    }

    public void ExecuteProgram()
    {
        while (IP < Program.Count)
        {
            switch (Program[IP])
            {
                case 0:
                    ExecuteAdv(Program[IP + 1]);
                    break;
                case 1:
                    ExecuteBxl(Program[IP + 1]);
                    break;
                case 2:
                    ExecuteBst(Program[IP + 1]);
                    break;
                case 3:
                    ExecuteJnz(Program[IP + 1]);
                    break;
                case 4:
                    ExecuteBxc();
                    break;
                case 5:
                    ExecuteOut(Program[IP + 1]);
                    break;
                case 6:
                    ExecuteBdv(Program[IP + 1]);
                    break;
                case 7:
                    ExecuteCdv(Program[IP + 1]);
                    break;
                default:
                    throw new Exception($"Invalid instruction {Program[IP]}");
            }
        }
    }

    private void ExecuteAdv(int operand)
    {
        A = A / (1L << (int)GetComboValue(operand));
        IP += 2;
    }

    private void ExecuteBxl(int operand)
    {
        B = B ^ operand;
        IP += 2;
    }

    private void ExecuteBst(int operand)
    {
        B = GetComboValue(operand) & 0x7;
        IP += 2;
    }

    private void ExecuteJnz(int operand)
    {
        if (A != 0)
        {
            IP = operand;
        }
        else
        {
            IP += 2;
        }
    }

    private void ExecuteBxc()
    {
        B = B ^ C;
        IP += 2;
    }

    private void ExecuteOut(int operand)
    {
        Output.Add(GetComboValue(operand) & 0x7);
        IP += 2;
    }

    private void ExecuteBdv(int operand)
    {
        B = A / (1L << (int)GetComboValue(operand));
        IP += 2;
    }

    private void ExecuteCdv(int operand)
    {
        C = A / (1L << (int)GetComboValue(operand));
        IP += 2;
    }

    private long GetComboValue(int operand)
    {
        switch (operand)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return operand;
            case 4:
                return A;
            case 5:
                return B;
            case 6:
                return C;
            default:
                throw new Exception($"invalid combo operand {operand}");
        }
    }

    public long FindAThatReplicatesProgram()
    {
        // Only works for the algorithm specified in the actual puzzle input.
        // Got inspiration from the subreddit:
        // The algorithm continuously processes the last 3 bits of A and
        // mangles those bits into something that is output. So we need to
        // build up A 8 bits at a time and check if that will result in the
        // next number matching up in the output.
        long a = 0;
        for (int programIndex = Program.Count - 1; programIndex >= 0; programIndex--)
        {
            // The 10 is from trial and error, the exception below will trigger when
            // only going up to 8.
            bool hasFoundMatchingSubA = false;
            for (int i = 0; i < 10; i++)
            {
                Computer testComputer = new(this)
                {
                    A = a + i
                };
                testComputer.ExecuteProgram();
                if (testComputer.Output.Count >= Program.Count - programIndex &&
                    testComputer.Output[testComputer.Output.Count - (Program.Count - programIndex)] == Program[programIndex])
                {
                    if (programIndex > 0)
                    {
                        a = (a + i) * 8;
                    }
                    else
                    {
                        a += i;
                    }
                    hasFoundMatchingSubA = true;
                    break;
                }
            }
            if (!hasFoundMatchingSubA)
            {
                throw new Exception($"Could not find a proper sub A for iteration {Program.Count - programIndex}");
            }
        }
        return a;
    }
}