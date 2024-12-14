namespace AoC;

public class Rect
{
    private readonly Point2d _topLeft;
    private readonly Point2d _bottomRight;

    public long Width => _bottomRight.X - _topLeft.X + 1;

    public long Height => _bottomRight.Y - _topLeft.Y + 1;

    public Point2d TopLeft => _topLeft;

    public Point2d BottomRight => _bottomRight;

    public Rect(Point2d topLeft, Point2d bottomRight)
    {
        _topLeft = topLeft;
        _bottomRight = bottomRight;
    }

    public bool Contains(Point2d point)
    {
        return point.X >= _topLeft.X && point.X <= _bottomRight.X &&
               point.Y >= _topLeft.Y && point.Y <= _bottomRight.Y;
    }

    public bool IsLeftOf(Point2d point)
    {
        return _bottomRight.X < point.X;
    }

    public bool IsAbove(Point2d point)
    {
        return _bottomRight.Y > point.Y;
    }

    public void Print()
    {
        Console.WriteLine($"Box: {_topLeft} to {_bottomRight}");
    }
}