namespace AoC;

public class Keypad(int nrRobots)
{
    private static readonly Dictionary<char, Point2d> lockCharToPointMap = new()
    {
        ['0'] = new Point2d(1, 3),
        ['1'] = new Point2d(0, 2),
        ['2'] = new Point2d(1, 2),
        ['3'] = new Point2d(2, 2),
        ['4'] = new Point2d(0, 1),
        ['5'] = new Point2d(1, 1),
        ['6'] = new Point2d(2, 1),
        ['7'] = new Point2d(0, 0),
        ['8'] = new Point2d(1, 0),
        ['9'] = new Point2d(2, 0),
        ['A'] = new Point2d(2, 3)
    };
    private const int InvalidYOfLock = 3;
    private readonly Dictionary<(char, char), List<string>> possibleLockMoves = GeneratePossibleMoves(lockCharToPointMap, InvalidYOfLock);

    private static readonly Dictionary<char, Point2d> directionalKeypadCharToPointMap = new()
    {
        ['^'] = new Point2d(1, 0),
        ['A'] = new Point2d(2, 0),
        ['<'] = new Point2d(0, 1),
        ['v'] = new Point2d(1, 1),
        ['>'] = new Point2d(2, 1)
    };
    private const int InvalidYOfPad = 0;
    private readonly Dictionary<(char, char), List<string>> possiblePadMoves = GeneratePossibleMoves(directionalKeypadCharToPointMap, InvalidYOfPad);

    private readonly Dictionary<(string, int), long> memoizationCache = [];

    private static Dictionary<(char, char), List<string>> GeneratePossibleMoves(Dictionary<char, Point2d> charToPointMap, int yOfInvalidTile)
    {
        Dictionary<(char, char), List<string>> possibleRoutes = [];

        foreach (char start in charToPointMap.Keys)
        {
            foreach (char end in charToPointMap.Keys)
            {
                if (start != end)
                {
                    possibleRoutes[(start, end)] = [];

                    Point2d p1 = charToPointMap[start];
                    Point2d p2 = charToPointMap[end];
                    List<string> sequences = GetCommandSequences(p1, p2, yOfInvalidTile);
                    possibleRoutes[(start, end)] = sequences;
                }
                else
                {
                    possibleRoutes[(start, end)] = ["A"];
                }
            }
        }

        return possibleRoutes;
    }

    public long GetHumanSequenceLength(string code)
    {
        return GetMinHumanSequenceLength($"A{code}", nrRobots);
    }

    public static void PrintCommandOutput(string commands)
    {
        List<Board> boards = [];
        for (int i = 0; i < 2; i++)
        {
            boards.Add(CreateDirectionalPadBoard());
        }
        boards.Add(CreateLockBoard());
        Print(boards);

        foreach (char command in commands)
        {
            ExecuteCommand(command, 0, boards);
            Print(boards);
            Console.WriteLine();
        }
    }

    private static void ExecuteCommand(char command, int boardIndex, List<Board> boards)
    {
        if (boardIndex == boards.Count)
        {
            Console.WriteLine($"Lockboard: {command}");
            return;
        }

        Board board = boards[boardIndex];
        switch (command)
        {
            case '^':
                board.Current = board.Current.Move(Direction.Up);
                break;
            case '>':
                board.Current = board.Current.Move(Direction.Right);
                break;
            case 'v':
                board.Current = board.Current.Move(Direction.Down);
                break;
            case '<':
                board.Current = board.Current.Move(Direction.Left);
                break;
            case 'A':
                char commandForNextBoard = board[board.Current];
                ExecuteCommand(commandForNextBoard, boardIndex + 1, boards);
                break;
        }

        if (!board.CanBeEntered(board.Current))
        {
            throw new Exception($"Trying to enter {board.Current.X},{board.Current.Y} on board {boardIndex}");
        }
    }

    private static void Print(List<Board> padBoards)
    {
        for (int i = 0; i < 4; i++)
        {
            IEnumerable<string> padBoardRows = padBoards.Select(p => p.ToString(i));
            Console.WriteLine($"{string.Join(' ', padBoardRows)}");
        }
    }

    private static Board CreateDirectionalPadBoard()
    {
        List<string> input = """
        #^A
        <v>
        """.Split('\n').ToList();
        return new Board(input);
    }

    private static Board CreateLockBoard()
    {
        List<string> input = """
        789
        456
        123
        #0A
        """.Split('\n').ToList();
        return new Board(input);
    }

    public static List<string> GetHumanSequences(string code, int codeIndex, int nrRobots)
    {
        List<string> resultSequences = [];
        if (codeIndex == code.Length - 1)
        {
            return resultSequences;
        }

        List<string> sequencesForCurrentTransition = GetHumanSequences(code[codeIndex], code[codeIndex + 1], nrRobots);
        foreach (string sequence in sequencesForCurrentTransition)
        {
            List<string> sequencesForRest = GetHumanSequences(code, codeIndex + 1, nrRobots);
            if (sequencesForRest.Any())
            {
                foreach (string sequenceForRest in sequencesForRest)
                {
                    resultSequences.Add($"{sequence}{sequenceForRest}");
                }
            }
            else
            {
                resultSequences.Add(sequence);
            }
        }

        return resultSequences;
    }

    public long GetMinHumanSequenceLength(string code, int nrRobots)
    {
        if (memoizationCache.TryGetValue((code, nrRobots), out long length))
        {
            return length;
        }

        if (nrRobots == 0)
        {
            // Don't count the 'A' we prepended to be able to compute the transition
            // from the start position of the robot's arm.
            return code.Length - 1;
        }

        List<List<string>> possibleMovesPerTileTransition = [];
        Dictionary<(char, char), List<string>> possibleMovesDict = char.IsAsciiDigit(code[1]) ? possibleLockMoves : possiblePadMoves;
        for (int i = 0; i < code.Length - 1; i++)
        {
            char start = code[i];
            char end = code[i + 1];
            possibleMovesPerTileTransition.Add(possibleMovesDict[(start, end)]);
        }

        long sequenceLength = long.MaxValue;
        List<List<string>> movesToTry = GenerateMovesToTry(possibleMovesPerTileTransition);
        foreach (List<string> moveToTry in movesToTry)
        {
            long thisSequenceLength = moveToTry.Sum(m => GetMinHumanSequenceLength($"A{m}", nrRobots - 1));
            if (thisSequenceLength < sequenceLength)
            {
                sequenceLength = thisSequenceLength;
            }
        }

        memoizationCache[(code, nrRobots)] = sequenceLength;

        return sequenceLength;
    }

    private List<List<string>> GenerateMovesToTry(List<List<string>> possibleMovesPerTileTransition)
    {
        if (possibleMovesPerTileTransition.Count == 1)
        {
            List<List<string>> result = [];
            foreach (string move in possibleMovesPerTileTransition[0])
            {
                result.Add([move]);
            }
            return result;
        }

        // Each position in possibleMovesPerTileTransition contains one or two possible moves to make the transition.
        // We have to generate all possible combinations.
        List<List<string>> movesToTry = [];
        foreach (string move in possibleMovesPerTileTransition[0])
        {
            List<List<string>> restMoves = GenerateMovesToTry(possibleMovesPerTileTransition.Skip(1).ToList());
            foreach (List<string> restMove in restMoves)
            {
                List<string> moves = [move];
                moves.AddRange(restMove);
                movesToTry.Add(moves);
            }
        }
        return movesToTry;
    }

    public static List<string> GetHumanSequences(char c1, char c2, int nrRobots)
    {
        Point2d p1 = lockCharToPointMap[c1];
        Point2d p2 = lockCharToPointMap[c2];
        List<string> lockCommands = GetCommandSequences(p1, p2, 3);
        List<string> metaCommands = lockCommands;
        for (int i = 0; i < nrRobots - 1; i++)
        {
            metaCommands = GetMetaCommandSequences(metaCommands);
        }
        return metaCommands;
    }

    private static List<string> GetMetaCommandSequences(List<string> sequences)
    {
        return sequences
            .SelectMany(s => GetMetaCommandSequences($"A{s}", 0, 0))
            .ToList();
    }

    private static List<string> GetMetaCommandSequences(string commands, int commandIndex, int yOfInvalidTile)
    {
        List<string> allSequences = [];
        if (commandIndex >= commands.Length - 1)
        {
            return allSequences;
        }
        Point2d p1 = directionalKeypadCharToPointMap[commands[commandIndex]];
        Point2d p2 = directionalKeypadCharToPointMap[commands[commandIndex + 1]];
        List<string> sequences = GetCommandSequences(p1, p2, yOfInvalidTile);
        foreach (string sequence in sequences)
        {
            List<string> restSequences = GetMetaCommandSequences(commands, commandIndex + 1, yOfInvalidTile);
            if (restSequences.Any())
            {
                foreach (string restSequence in restSequences)
                {
                    allSequences.Add($"{sequence}{restSequence}");
                }
            }
            else
            {
                allSequences.Add(sequence);
            }
        }

        return allSequences;
    }

    public static List<string> GetCommandSequences(Point2d p1, Point2d p2, int yOfInvalidTile)
    {
        if (p1 == p2)
        {
            return ["A"];
        }

        List<string> commands = [];
        Point2d delta = p2 - p1;
        bool canDoHorizontalMovementFirst = !CheckHorizontalMovementFirstWillVisitInvalidTile(p1, p2, yOfInvalidTile);
        if (canDoHorizontalMovementFirst && delta.X != 0)
        {
            commands.Add(GetCommandSequenceHorizontalMovementFirst(p1, p2));
        }

        bool canDoVerticalMovementFirst = !CheckVerticalMovementFirstWillVisitInvalidTile(p1, p2, yOfInvalidTile);
        if (canDoVerticalMovementFirst && delta.Y != 0)
        {
            commands.Add(GetCommandSequenceVerticalMovementFirst(p1, p2));
        }

        return commands;
    }

    private static bool CheckHorizontalMovementFirstWillVisitInvalidTile(Point2d p1, Point2d p2, int yOfInvalidTile)
    {
        return p2.X == 0 && p1.Y == yOfInvalidTile;
    }

    private static bool CheckVerticalMovementFirstWillVisitInvalidTile(Point2d p1, Point2d p2, int yOfInvalidTile)
    {
        return p1.X == 0 && p2.Y == yOfInvalidTile;
    }
    private static string GetCommandSequenceHorizontalMovementFirst(Point2d p1, Point2d p2)
    {
        Point2d delta = p2 - p1;
        string commands = "";

        if (delta.X > 0)
        {
            commands += new string('>', (int)delta.X);
        }
        if (delta.X < 0)
        {
            commands += new string('<', (int)-delta.X);
        }

        if (delta.Y > 0)
        {
            commands += new string('v', (int)delta.Y);
        }
        else if (delta.Y < 0)
        {
            commands += new string('^', (int)-delta.Y);
        }

        commands += 'A';
        return commands;
    }

    private static string GetCommandSequenceVerticalMovementFirst(Point2d p1, Point2d p2)
    {
        Point2d delta = p2 - p1;
        string commands = "";

        if (delta.Y > 0)
        {
            commands += new string('v', (int)delta.Y);
        }
        else if (delta.Y < 0)
        {
            commands += new string('^', (int)-delta.Y);
        }

        if (delta.X > 0)
        {
            commands += new string('>', (int)delta.X);
        }
        if (delta.X < 0)
        {
            commands += new string('<', (int)-delta.X);
        }

        commands += 'A';
        return commands;
    }
}
