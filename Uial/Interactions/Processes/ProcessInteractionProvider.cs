using System.Collections.Generic;

namespace Uial.Interactions.Processes;

public class ProcessInteractionProvider : BaseInteractionProvider
{
    protected override IDictionary<string, InteractionFactory> KnownInteractions => new Dictionary<string, InteractionFactory>()
    {
        { StartProcess.Key, (_, _, paramValues) => StartProcess.FromRuntimeValues(paramValues) },
    };
}

