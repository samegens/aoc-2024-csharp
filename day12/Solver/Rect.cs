namespace AoC;

public class Rect
{
    private readonly Point2dI _topLeft;
    private readonly Point2dI _bottomRight;

    public int Width => _bottomRight.X - _topLeft.X + 1;

    public int Height => _bottomRight.Y - _topLeft.Y + 1;

    public Point2dI TopLeft => _topLeft;

    public Point2dI BottomRight => _bottomRight;

    public Rect(Point2dI topLeft, Point2dI bottomRight)
    {
        _topLeft = topLeft;
        _bottomRight = bottomRight;
    }

    public bool Contains(Point2dI point)
    {
        return point.X >= _topLeft.X && point.X <= _bottomRight.X &&
               point.Y >= _topLeft.Y && point.Y <= _bottomRight.Y;
    }

    public bool IsLeftOf(Point2dI point)
    {
        return _bottomRight.X < point.X;
    }

    public bool IsAbove(Point2dI point)
    {
        return _bottomRight.Y > point.Y;
    }

    public void Print()
    {
        Console.WriteLine($"Box: {_topLeft} to {_bottomRight}");
    }

    public bool EdgesTouch(Rect boundingBox)
    {
        return TopLeft.X == boundingBox.TopLeft.X ||
               TopLeft.Y == boundingBox.TopLeft.Y ||
               BottomRight.X == boundingBox.BottomRight.X ||
               BottomRight.Y == boundingBox.BottomRight.Y;
    }

    public bool SingleEdgeTouches(Rect boundingBox)
    {
        return TopLeft.X == boundingBox.TopLeft.X ^
               TopLeft.Y == boundingBox.TopLeft.Y ^
               BottomRight.X == boundingBox.BottomRight.X ^
               BottomRight.Y == boundingBox.BottomRight.Y;
    }
}
