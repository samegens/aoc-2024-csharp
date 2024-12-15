namespace AoC;

public enum Direction
{
    Up, Right, Down, Left
}

public static class DirectionHelpers
{
    public static readonly Dictionary<Direction, Point2d> Movements = new()
    {
        [Direction.Up] = new Point2d(0, -1),
        [Direction.Right] = new Point2d(1, 0),
        [Direction.Down] = new Point2d(0, 1),
        [Direction.Left] = new Point2d(-1, 0),
    };

    public static readonly Dictionary<Direction, char> Chars = new()
    {
        [Direction.Up] = '^',
        [Direction.Right] = '>',
        [Direction.Down] = 'v',
        [Direction.Left] = '<',
    };

    public static readonly Dictionary<char, Direction> CharToDirectionMap = new()
    {
        ['^'] = Direction.Up,
        ['>'] = Direction.Right,
        ['v'] = Direction.Down,
        ['<'] = Direction.Left
    };

    public static List<Direction> Parse(List<string> lines)
    {
        string input = string.Join("", lines.Select(l => l.Trim()));
        return input.Select(ch => CharToDirectionMap[ch]).ToList();
    }
}