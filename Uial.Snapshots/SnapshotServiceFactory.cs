using Uial.Snapshots.Internal;

namespace Uial.Snapshots;

internal class SnapshotServiceFactory
{
    public static ISnapshotService CreateSnapshotService()
    {
        ElementPropertiesProvider elementPropertiesProvider = new();
        ScreenshotService screenshotService = new();
        IUITreeService uiTreeService = new LiveUITreeService(elementPropertiesProvider);
        ScreenHelper screenHelper = new();
        FolderConfig folderConfig = new();
        return new SnapshotService(screenshotService, elementPropertiesProvider, uiTreeService, screenHelper, folderConfig, shouldSaveAsSingleFile: false);
    }
}
