using System;
using System.Collections.Generic;
using Uial.Interactions;

namespace Uial.Testing
{
    public class Test : ITestable
    {
        public string Name { get; protected set; }

        protected IEnumerable<IInteraction> Interactions { get; set; }

        public Test(string name, IEnumerable<IInteraction> interactions)
        {
            if (name == null || interactions == null) 
            {
                throw new ArgumentNullException(name == null ? "name" : "interactions");
            }
            Name = name;
            Interactions = interactions;
        }

        public ITestResults RunTest()
        {
            bool passed = true;
            foreach (IInteraction interaction in Interactions)
            {
                try
                {
                    interaction.Do();
                }
                catch (Exception e)
                {
                    passed = false;
                    break;
                }
            }

            return new TestResults(Name, passed);
        }
    }
}
