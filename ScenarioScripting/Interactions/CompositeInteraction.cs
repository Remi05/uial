﻿using System;
using System.Collections.Generic;

namespace ScenarioScripting.Interactions
{
    public class CompositeInteraction : IInteraction
    {
        public string Name { get; protected set; }

        protected IEnumerable<IInteraction> Interactions { get; set; }

        public CompositeInteraction(string name, IEnumerable<IInteraction> interactions)
        {
            if (name == null || interactions == null)
            {
                throw new ArgumentNullException(name == null ? "name" : "interactions");
            }
            Name = name;
            Interactions = interactions;
        }

        public void Do()
        {
            foreach (IInteraction interaction in Interactions)
            {
                interaction.Do();
            }
        }
    }
}
