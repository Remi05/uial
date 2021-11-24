using Uial.Definitions;

namespace Uial.Parsing
{
    public interface IScriptParser
    {
        Script ParseScript(string filePath);
    }
}
