namespace AoC;

public enum Direction
{
    Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft
}

public static class DirectionHelpers
{
    public static readonly List<Direction> EightDirections = [
        Direction.Up,
        Direction.UpRight,
        Direction.Right,
        Direction.DownRight,
        Direction.Down,
        Direction.DownLeft,
        Direction.Left,
        Direction.UpLeft
    ];

    // Assumes axes where top left is origin.
    public static readonly Dictionary<Direction, Point> Movements = new()
    {
        [Direction.Up] = new Point(0, -1),
        [Direction.UpRight] = new Point(1, -1),
        [Direction.Right] = new Point(1, 0),
        [Direction.DownRight] = new Point(1, 1),
        [Direction.Down] = new Point(0, 1),
        [Direction.DownLeft] = new Point(-1, 1),
        [Direction.Left] = new Point(-1, 0),
        [Direction.UpLeft] = new Point(-1, -1)
    };
}