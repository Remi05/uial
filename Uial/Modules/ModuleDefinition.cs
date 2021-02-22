using System.Collections.Generic;

namespace Uial.Modules
{
    public class ModuleDefinition
    {
        public string Name { get; private set; }
        public string BinaryPath { get; private set; }

        public ModuleDefinition(string name, string binaryPath)
        {
            Name = name;
            BinaryPath = binaryPath;
        }
    }
}
