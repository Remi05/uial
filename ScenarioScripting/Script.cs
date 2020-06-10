using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScenarioScripting.Contexts;

namespace ScenarioScripting
{
    public class Script
    {
        public Dictionary<string, Scenario> Scenarios { get; private set; }

        public Dictionary<string, IContext> Contexts { get; private set; }
    }
}
