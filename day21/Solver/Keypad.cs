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

    private static List<List<string>> GenerateMovesToTry(List<List<string>> possibleMovesPerTileTransition)
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
