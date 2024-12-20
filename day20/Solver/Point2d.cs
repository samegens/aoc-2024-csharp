
namespace AoC;

public record Point2d(long X, long Y)
{
    public Point2d(Point2d point)
    {
        X = point.X;
        Y = point.Y;
    }

    public Point2d Move(long dX, long dY) => new(X + dX, Y + dY);
    public Point2d Move(Point2d delta) => new(X + delta.X, Y + delta.Y);
    public Point2d Move(Direction direction) => Move(DirectionHelpers.Movements[direction]);
    public Point2d MoveOpposite(Direction direction) => Move(DirectionHelpers.Movements[DirectionHelpers.Opposite[direction]]);

    public static Point2d operator -(Point2d p1, Point2d p2) => new(p1.X - p2.X, p1.Y - p2.Y);
    public static Point2d operator +(Point2d p1, Point2d p2) => new(p1.X + p2.X, p1.Y + p2.Y);

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public void Print()
    {
        Console.WriteLine($"{X},{Y}");
    }

    public long ManhattanDistanceTo(Point2d p2) => Math.Abs(p2.X - X) + Math.Abs(p2.Y - Y);
}