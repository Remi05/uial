using System;
using System.Collections.Generic;
using System.Drawing;
using UIAutomationClient;

namespace Uial.Snapshots.Internal;

public class LiveUITreeService : IUITreeService
{
    private IUIAutomation UIAutomation { get; } = new CUIAutomation();
    private ElementPropertiesProvider ElementPropertiesProvider { get; }
    private IUIAutomationCondition ConstantFilterCondition { get; }
    public IUITreeNode RootNode { get; }

    public LiveUITreeService(ElementPropertiesProvider elementPropertiesProvider)
    {
        if (elementPropertiesProvider == null)
        {
            throw new ArgumentNullException(nameof(elementPropertiesProvider));
        }
        ElementPropertiesProvider = elementPropertiesProvider;
        ConstantFilterCondition = UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_IsOffscreenPropertyId, false);
        RootNode = new LiveUITreeNode(UIAutomation.GetRootElement(), ElementPropertiesProvider, ConstantFilterCondition);
    }

    public CachedUITreeNode CreateSnapshotOfElementSubTree(ElementProperties rootElementProperties)
    {
        AutomationElementProperties rootAutomationElementProperties = rootElementProperties as AutomationElementProperties;
        if (rootAutomationElementProperties == null)
        {
            return null;
        }
        try
        {
            var childrenElements = rootAutomationElementProperties.Element.FindAll(TreeScope.TreeScope_Children, ConstantFilterCondition);
            var childrenNodes = new List<CachedUITreeNode>();

            for (int i = 0; i < childrenElements.Length; ++i)
            {
                var childElementProperties = ElementPropertiesProvider.GetElementProperties(childrenElements.GetElement(i));
                CachedUITreeNode childNode = CreateSnapshotOfElementSubTree(childElementProperties);
                if (childNode != null)
                {
                    childrenNodes.Add(childNode);
                }
            }

            return new CachedUITreeNode() { ElementProperties = rootAutomationElementProperties, Children = childrenNodes };
        }
        catch
        {
            // TODO: Consider logging the failure to create the subtree.
            return null;
        }
    }

    public CachedUITreeNode CreateSnapshotOfSubTreeInBounds(Rectangle bounds)
    {
        var childrenNodes = CreateSnapshotOfSubTreeInBounds(bounds, UIAutomation.GetRootElement());
        var rootProperties = new ElementProperties() { Name = "Root", BoundingRect = bounds };
        return new CachedUITreeNode() { ElementProperties = rootProperties, Children = childrenNodes };
    }

    private List<CachedUITreeNode> CreateSnapshotOfSubTreeInBounds(Rectangle bounds, IUIAutomationElement curElement)
    {
        var elementsInBounds = new List<CachedUITreeNode>();
        try
        {
            Rectangle curElementBounds = curElement.CurrentBoundingRectangle.ToDrawingRectangle();
            if (bounds.IntersectsWith(curElementBounds))
            {
                var childrenElements = curElement.FindAll(TreeScope.TreeScope_Children, ConstantFilterCondition);
                var childrenNodes = new List<CachedUITreeNode>();

                for (int i = 0; i < childrenElements.Length; ++i)
                {
                    var childElementProperties = ElementPropertiesProvider.GetElementProperties(childrenElements.GetElement(i));
                    CachedUITreeNode childNode = CreateSnapshotOfElementSubTree(childElementProperties);
                    if (childNode != null)
                    {
                        childrenNodes.Add(childNode);
                    }
                }

                ElementProperties curElementProperties = ElementPropertiesProvider.GetElementProperties(curElement);
                CachedUITreeNode curNode = new CachedUITreeNode() { ElementProperties = curElementProperties, Children = childrenNodes };
                elementsInBounds.Add(curNode);
            }
            else
            {
                var childrenElements = curElement.FindAll(TreeScope.TreeScope_Children, ConstantFilterCondition);
                for (int i = 0; i < childrenElements.Length; ++i)
                {
                    var subTreeInBounds = CreateSnapshotOfSubTreeInBounds(bounds, childrenElements.GetElement(i));
                    foreach (var childNode in subTreeInBounds)
                    {
                        elementsInBounds.Add(childNode);
                    }
                }
            }
        }
        catch
        {
            // TODO: Consider logging the failure to create the subtree.
            return null;
        }

        return elementsInBounds;
    }
}
