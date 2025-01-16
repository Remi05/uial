using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Uial.Interactions.Processes;

public class StartProcess : IInteraction
{
    public const string Key = "StartProcess";

    public string Name => Key;
    public string Command { get; protected set; }

    public StartProcess(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            throw new ArgumentException($"\"{nameof(command)}\" parameter is null or empty.");
        }
        Command = command;
    }

    public void Do()
    {
        ProcessStartInfo processStartInfo = new(Command) { UseShellExecute = true };
        Process.Start(processStartInfo);
    }

    public static StartProcess FromRuntimeValues(IEnumerable<string> paramValues)
    {
        if (paramValues.Count() != 1)
        {
            throw new InvalidParameterCountException(1, paramValues.Count());
        }
        return new StartProcess(paramValues.ElementAt(0));
    }
}
