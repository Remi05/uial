using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Mouse
{
    public class MouseInteractionProvider : IInteractionProvider
    {
        protected IMouseService MouseService { get; set; }

        protected IDictionary<string, IInteraction> KnownInteractions = new Dictionary<string, IInteraction>();

        public MouseInteractionProvider(IMouseService mouseService)
        {
            MouseService = mouseService;
            KnownInteractions.Add("Click", new SkeletonInteraction("Click", MouseService.Click));
            KnownInteractions.Add("DoubleClick", new SkeletonInteraction("DoubleClick", MouseService.DoubleClick));
            //KnownInteractions.Add("Drag", new SkeletonInteraction("Drag", MouseService.Drag));
            //KnownInteractions.Add("DragTo", new SkeletonInteraction("DragTo", MouseService.DragTo));
            //KnownInteractions.Add("Move", new SkeletonInteraction("Move", MouseService.Move));
            //KnownInteractions.Add("MoveTo", new SkeletonInteraction("MoveTo", MouseService.MoveTo));
            KnownInteractions.Add("Press", new SkeletonInteraction("Press", MouseService.Press));
            KnownInteractions.Add("Release", new SkeletonInteraction("Release", MouseService.Release));
        }

        public bool IsKnownInteraction(string interactionName)
        {
            return KnownInteractions.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {   
            if (!IsKnownInteraction(interactionName))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            return KnownInteractions[interactionName];
        }
    }
}
