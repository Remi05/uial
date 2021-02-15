using System;

namespace Uial.Interactions
{
    public class SkeletonInteraction : IInteraction
    {
        public string Name { get; private set; }
        private Action Action { get; set; }

        public SkeletonInteraction(string name, Action action)
        {
            if (name == null || action == null)
            {
                throw new ArgumentNullException(name == null ? nameof(name) : nameof(action));
            }
            Name = name;
            Action = action;
        }

        public void Do()
        {
            Action();
        }
    }
}
