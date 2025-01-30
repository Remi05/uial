using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Uial.Contexts.Windows;
using Uial.Interactions.Windows;
using Uial.Snapshots.Internal;

namespace Uial.Snapshots;

public class ScreenshotInteraction : AbstractInteraction
{
    public const string Key = "TakeScreenshot";

    public override string Name => Key;

    protected ScreenshotService ScreenshotService = new();
    protected FolderConfig FolderConfig = new();

    public ScreenshotInteraction(IWindowsVisualContext context)
        : base(context) { }

    public override void Do()
    {
        base.Do();
        Rectangle screenshotRect = Context.RootElement.CurrentBoundingRectangle.ToDrawingRectangle();
        Image screenshot = ScreenshotService.TakeScreenshot(screenshotRect);

        string fileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
        string filePath = Path.Join(FolderConfig.GetScreenshotsFolderPath(), fileName);
        screenshot.Save(filePath, ImageFormat.Png);
    }
}
