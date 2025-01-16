using System.Drawing;

namespace Uial.Snapshots.Internal;

public delegate void RootNodeChangedEventHandler();

public interface IUITreeService
{
    IUITreeNode RootNode { get; }

    CachedUITreeNode CreateSnapshotOfElementSubTree(ElementProperties rootElementProperties);
    CachedUITreeNode CreateSnapshotOfSubTreeInBounds(Rectangle bounds);
}