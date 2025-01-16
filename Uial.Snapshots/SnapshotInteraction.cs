using Uial.Contexts.Windows;
using Uial.Interactions.Windows;
using Uial.Snapshots.Internal;

namespace Uial.Snapshots;

public class SnapshotInteraction : AbstractInteraction
{
    public const string Key = "TakeSnapshot";

    public override string Name => Key;

    protected ISnapshotService SnapshotService = SnapshotServiceFactory.CreateSnapshotService();

    public SnapshotInteraction(IWindowsVisualContext context)
        : base(context) { }

    public override void Do()
    {
        base.Do();
        Snapshot snapshot = SnapshotService.TakeSnapshot(Context.RootElement);
        SnapshotService.SaveSnapshot(snapshot);
    }
}
