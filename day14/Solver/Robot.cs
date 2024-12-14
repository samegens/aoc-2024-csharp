using System.Text.RegularExpressions;

namespace AoC;

public record class Robot(Point2dL Position, Point2dL Velocity)
{
    public static long AreaWidth = 101;
    public static long AreaHeight = 103;

    public int Quadrant
    {
        get
        {
            if (Position.X < AreaWidth / 2 && Position.Y < AreaHeight / 2)
            {
                return 1;
            }
            if (Position.X > AreaWidth / 2 && Position.Y < AreaHeight / 2)
            {
                return 2;
            }
            if (Position.X < AreaWidth / 2 && Position.Y > AreaHeight / 2)
            {
                return 3;
            }
            if (Position.X > AreaWidth / 2 && Position.Y > AreaHeight / 2)
            {
                return 4;
            }
            return 0;
        }
    }

    public static Robot Parse(string input)
    {
        Match match = Regex.Match(input, @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
        long x = long.Parse(match.Groups[1].Value);
        long y = long.Parse(match.Groups[2].Value);
        long vx = long.Parse(match.Groups[3].Value);
        long vy = long.Parse(match.Groups[4].Value);
        return new(new Point2dL(x, y), new Point2dL(vx, vy));
    }

    public Robot Move(long nrMoves)
    {
        // long pos = Position.X + Position.Y * AreaWidth;
        // long delta = Velocity.X + Velocity.Y * AreaWidth;
        // long newPos = (pos + nrMoves * delta) % (AreaHeight * AreaWidth);
        // if (newPos < 0)
        // {
        //     newPos += AreaHeight * AreaWidth;
        // }

        // long newX = newPos % AreaWidth;
        // // When X is increased beyond the edge of a row, we now inadvertently also increase
        // // the Y. So we have to reduce Y with the number of times we have teleported across
        // // the row.
        // long nrHorizontalTeleports = (Position.X + Velocity.X * nrMoves) / AreaWidth;
        // long newY = (newPos - newX - nrHorizontalTeleports) / AreaWidth;
        long newX = (Position.X + nrMoves * Velocity.X) % AreaWidth;
        if (newX < 0)
        {
            newX += AreaWidth;
        }
        long newY = (Position.Y + nrMoves * Velocity.Y) % AreaHeight;
        if (newY < 0)
        {
            newY += AreaHeight;
        }

        return new Robot(new Point2dL(newX, newY), Velocity);
    }

    // public 
}