namespace AoC;

public class Plot
{
    private readonly HashSet<Point2dI> _positions;

    public Plot(IEnumerable<Point2dI> positions)
    {
        _positions = new(positions);
        int minX = _positions.Min(p => p.X);
        int minY = _positions.Min(p => p.Y);
        int maxX = _positions.Max(p => p.X);
        int maxY = _positions.Max(p => p.Y);
        BoundingBox = new Rect(new Point2dI(minX, minY), new Point2dI(maxX, maxY));
    }

    public Rect BoundingBox { get; private set; }

    public HashSet<Point2dI> Positions => _positions;

    public int Area => _positions.Count;

    public int Price => Perimeter * Area;

    public int BulkDiscountPrice => NrSides * Area;

    public int Perimeter => GetHorizontalPerimeterCount() + GetVerticalPerimeterCount();

    private int GetHorizontalPerimeterCount()
    {
        int perimeter = 0;
        for (int y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
        {
            bool isInPlot = false;
            for (int x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
            {
                Point2dI p = new(x, y);
                if (_positions.Contains(p) && !isInPlot)
                {
                    isInPlot = true;
                    perimeter++;
                }
                else if (!_positions.Contains(p) && isInPlot)
                {
                    isInPlot = false;
                    perimeter++;
                }
            }
            if (isInPlot)
            {
                perimeter++;
            }
        }

        return perimeter;
    }

    private int GetVerticalPerimeterCount()
    {
        int perimeter = 0;
        for (int x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            bool isInPlot = false;
            for (int y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Point2dI p = new(x, y);
                if (_positions.Contains(p) && !isInPlot)
                {
                    isInPlot = true;
                    perimeter++;
                }
                else if (!_positions.Contains(p) && isInPlot)
                {
                    isInPlot = false;
                    perimeter++;
                }
            }
            if (isInPlot)
            {
                perimeter++;
            }
        }

        return perimeter;
    }

    public int NrSides => GetHorizontalSideCount() + GetVerticalSideCount();

    private int GetHorizontalSideCount()
    {
        (List<Point2dI> edgesOutIn, List<Point2dI> edgesInOut) = GetHorizontalEdges();
        return GetHorizontalSideCount(edgesOutIn) + GetHorizontalSideCount(edgesInOut);
    }

    private int GetHorizontalSideCount(IEnumerable<Point2dI> edges)
    {
        int nrSides = 0;
        for (int x = edges.Min(p => p.X); x <= edges.Max(p => p.X); x++)
        {
            bool isInSide = false;
            for (int y = edges.Min(p => p.Y); y <= edges.Max(p => p.Y); y++)
            {
                Point2dI p = new(x, y);
                if (edges.Contains(p) && !isInSide)
                {
                    isInSide = true;
                    nrSides++;
                }
                else if (!edges.Contains(p) && isInSide)
                {
                    isInSide = false;
                }
            }
        }
        return nrSides;
    }

    private int GetVerticalSideCount()
    {
        (List<Point2dI> edgesOutIn, List<Point2dI> edgesInOut) = GetVerticalEdges();
        return GetVerticalSideCount(edgesOutIn) + GetVerticalSideCount(edgesInOut);
    }

    private int GetVerticalSideCount(IEnumerable<Point2dI> edges)
    {

        int nrSides = 0;
        for (int y = edges.Min(p => p.Y); y <= edges.Max(p => p.Y); y++)
        {
            bool isInSide = false;
            for (int x = edges.Min(p => p.X); x <= edges.Max(p => p.X); x++)
            {
                Point2dI p = new(x, y);
                if (edges.Contains(p) && !isInSide)
                {
                    isInSide = true;
                    nrSides++;
                }
                else if (!edges.Contains(p) && isInSide)
                {
                    isInSide = false;
                }
            }
        }
        return nrSides;
    }

    private (List<Point2dI> edgesOutIn, List<Point2dI> edgesInOut) GetHorizontalEdges()
    {
        List<Point2dI> edgesOutIn = [];
        List<Point2dI> edgesInOut = [];

        for (int y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
        {
            bool isInPlot = false;
            for (int x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
            {
                Point2dI p = new(x, y);
                if (_positions.Contains(p) && !isInPlot)
                {
                    isInPlot = true;
                    edgesOutIn.Add(new Point2dI(x, y));
                }
                else if (!_positions.Contains(p) && isInPlot)
                {
                    isInPlot = false;
                    edgesInOut.Add(new Point2dI(x, y));
                }
            }
            if (isInPlot)
            {
                edgesInOut.Add(new Point2dI(BoundingBox.BottomRight.X + 1, y));
            }
        }

        return (edgesOutIn, edgesInOut);
    }

    private (List<Point2dI> edgesOutIn, List<Point2dI> edgesInOut) GetVerticalEdges()
    {
        List<Point2dI> edgesOutIn = [];
        List<Point2dI> edgesInOut = [];

        for (int x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            bool isInPlot = false;
            for (int y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Point2dI p = new(x, y);
                if (_positions.Contains(p) && !isInPlot)
                {
                    isInPlot = true;
                    edgesOutIn.Add(new Point2dI(x, y));
                }
                else if (!_positions.Contains(p) && isInPlot)
                {
                    isInPlot = false;
                    edgesInOut.Add(new Point2dI(x, y));
                }
            }
            if (isInPlot)
            {
                edgesInOut.Add(new Point2dI(x, BoundingBox.BottomRight.Y + 1));
            }
        }

        return (edgesOutIn, edgesInOut);
    }

    public override string ToString()
    {
        return string.Join(';', _positions);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Plot other = (Plot)obj;
        if (other.Positions.Count != Positions.Count)
        {
            return false;
        }

        foreach (Point2dI p in Positions)
        {
            if (!other.Positions.Contains(p))
            {
                return false;
            }
        }
        return true;
    }

    public override int GetHashCode()
    {
        return _positions.GetHashCode();
    }
}
