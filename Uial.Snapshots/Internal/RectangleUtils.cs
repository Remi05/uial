using System.Drawing;

namespace Uial.Snapshots.Internal;

public static class RectangleUtils
{
    public static Point BottomLeft(this Rectangle rect)
    {
        return new Point(rect.Left, rect.Bottom);
    }

    public static Point BottomRight(this Rectangle rect)
    {
        return new Point(rect.Right, rect.Bottom);
    }

    public static Point TopLeft(this Rectangle rect)
    {
        return new Point(rect.Left, rect.Top);
    }

    public static Point TopRight(this Rectangle rect)
    {
        return new Point(rect.Right, rect.Top);
    }
}
