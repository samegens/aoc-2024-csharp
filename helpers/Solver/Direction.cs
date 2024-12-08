namespace AoC;

public enum Direction
{
    Up, Right, Down, Left
}

public static class DirectionHelpers
{
    public static readonly Dictionary<Direction, Point2dI> Movements = new()
    {
        [Direction.Up] = new Point2dI(0, -1),
        [Direction.Right] = new Point2dI(1, 0),
        [Direction.Down] = new Point2dI(0, 1),
        [Direction.Left] = new Point2dI(-1, 0),
    };

    public static readonly Dictionary<Direction, char> Chars = new()
    {
        [Direction.Up] = '^',
        [Direction.Right] = '>',
        [Direction.Down] = 'v',
        [Direction.Left] = '<',
    };
}