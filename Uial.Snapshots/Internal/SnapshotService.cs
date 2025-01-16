using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;
using UIAutomationClient;

namespace Uial.Snapshots.Internal;

public class SnapshotService : ISnapshotService
{
    private ScreenshotService ScreenshotService { get; }
    private ElementPropertiesProvider ElementPropertiesProvider { get; }
    private IUITreeService UITreeService { get; }
    private ScreenHelper ScreenHelper { get; }
    private FolderConfig FolderConfig { get; }
    private bool ShouldSaveAsSingleFile { get; }

    public SnapshotService(ScreenshotService screenshotService, ElementPropertiesProvider elementPropertiesProvider, IUITreeService uiTreeService, ScreenHelper screenHelper, FolderConfig folderConfig, bool shouldSaveAsSingleFile = true)
    {
        if (screenshotService == null || uiTreeService == null || screenHelper == null || folderConfig == null)
        {
            throw new ArgumentNullException(screenshotService == null ? nameof(screenshotService)
                                          : uiTreeService == null ? nameof(uiTreeService)
                                          : screenHelper == null ? nameof(screenHelper)
                                          : nameof(folderConfig));
        }
        ScreenshotService = screenshotService;
        ElementPropertiesProvider = elementPropertiesProvider;
        UITreeService = uiTreeService;
        ScreenHelper = screenHelper;
        FolderConfig = folderConfig;
        ShouldSaveAsSingleFile = shouldSaveAsSingleFile;
    }

    public Snapshot TakeSnapshot(Rectangle bounds)
    {
        CachedUITreeNode subtree = UITreeService.CreateSnapshotOfSubTreeInBounds(bounds);
        Image screenshot = ScreenshotService.TakeScreenshot(bounds);
        double scaleFactor = ScreenHelper.GetDisplayScaleFactor();
        return new Snapshot() { UITree = subtree, Screenshot = screenshot, ScaleFactor = scaleFactor };
    }

    public Snapshot TakeSnapshot(IUIAutomationElement element)
    {
        ElementProperties elementProperties = ElementPropertiesProvider.GetElementProperties(element);
        CachedUITreeNode subtree = UITreeService.CreateSnapshotOfElementSubTree(elementProperties);
        Image screenshot = ScreenshotService.TakeScreenshot(elementProperties);
        double scaleFactor = ScreenHelper.GetDisplayScaleFactor();
        return new Snapshot() { UITree = subtree, Screenshot = screenshot, ScaleFactor = scaleFactor };
    }

    public string SaveSnapshot(Snapshot snapshot)
    {
        if (ShouldSaveAsSingleFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                snapshot.Screenshot.Save(memoryStream, ImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();
                snapshot.ScreenshotBase64 = Convert.ToBase64String(imageBytes);
            }
        }
        else
        {
            EnsureScreenshotIsSavedAsFile(snapshot);
        }

        string snapshotJson = JsonSerializer.Serialize(snapshot);
        string fileName = GetSnapshotFileName(snapshot);
        string filePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), fileName);
        File.WriteAllText(filePath, snapshotJson);

        return filePath;
    }

    private string GetSnapshotFileName(Snapshot snapshot)
    {
        string snapshotName = "Snapshot";
        if (!string.IsNullOrWhiteSpace(snapshot.UITree.ElementProperties.Name))
        {
            snapshotName = snapshot.UITree.ElementProperties.Name;
        }
        return $"{snapshotName}-{DateTime.Now.ToFileTime()}.snpt";
    }

    private void EnsureScreenshotIsSavedAsFile(Snapshot snapshot)
    {
        if (string.IsNullOrWhiteSpace(snapshot.ScreenshotFilePath) || !File.Exists(snapshot.ScreenshotFilePath))
        {
            string screenshotFileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
            string screenshotFilePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), screenshotFileName);
            snapshot.Screenshot.Save(screenshotFilePath, ImageFormat.Png);
            snapshot.ScreenshotFilePath = screenshotFilePath;
        }
    }
}
