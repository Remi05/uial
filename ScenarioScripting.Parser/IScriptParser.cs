using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioScripting.Parser
{
    public interface IScriptParser
    {
        Script ParseScript(string filePath);
    }
}
