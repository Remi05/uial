
namespace Uial.Definitions
{
    public class ModuleDefinition
    {
        public string ModuleName { get; private set; }
        public string BinaryPath { get; private set; }

        public ModuleDefinition(string moduleName, string binaryPath)
        {
            ModuleName = moduleName;
            BinaryPath = binaryPath;
        }
    }
}
