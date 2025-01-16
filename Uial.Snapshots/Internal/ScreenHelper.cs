using System;
using System.Drawing;

namespace Uial.Snapshots.Internal;

public class ScreenHelper
{
    public double GetDisplayScaleFactor()
    {
        return Graphics.FromHwnd(IntPtr.Zero).DpiX / 96.0; // A scale factor of 100% has a DPI of 96.
    }
}
