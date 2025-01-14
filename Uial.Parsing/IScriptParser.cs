using Uial.DataModels;

namespace Uial.Parsing
{
    public interface IScriptParser
    {
        Script ParseScript(string filePath);
    }
}
