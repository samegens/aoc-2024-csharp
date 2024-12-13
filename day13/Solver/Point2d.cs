global using Point2dI = AoC.Point2d<int>;
global using Point2dL = AoC.Point2d<long>;

namespace AoC;

public record Point2d<T>(T X, T Y) where T : System.Numerics.IBinaryInteger<T>
{
    public Point2d(Point2d<T> point)
    {
        X = point.X;
        Y = point.Y;
    }

    public Point2d<T> Move(T dX, T dY) => new(X + dX, Y + dY);
    public Point2d<T> Move(Point2dI delta) => new(X + T.CreateChecked(delta.X), Y + T.CreateChecked(delta.Y));

    public static Point2d<T> operator -(Point2d<T> p1, Point2d<T> p2) => new(p1.X - p2.X, p1.Y - p2.Y);
    public static Point2d<T> operator +(Point2d<T> p1, Point2d<T> p2) => new(p1.X + p2.X, p1.Y + p2.Y);

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public void Print()
    {
        Console.WriteLine($"{X},{Y}");
    }
}