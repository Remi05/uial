using UIAutomationClient;

namespace Uial.Snapshots.Internal;

public interface ISnapshotService
{
    Snapshot TakeSnapshot(IUIAutomationElement element);
    string SaveSnapshot(Snapshot snapshot);
}