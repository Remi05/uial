using System.Collections.Generic;

namespace Uial.Interactions.Core;

public class CoreInteractionProvider : BaseInteractionProvider
{
    protected override IDictionary<string, InteractionFactory> KnownInteractions => new Dictionary<string, InteractionFactory>()
    {
        { IsAvailable.Key,         (context, scope, paramValues) => IsAvailable.FromRuntimeValues(context, scope, paramValues) },
        { Wait.Key,                (_, _, paramValues) => Wait.FromRuntimeValues(paramValues) },
        { WaitUntilAvailable.Key,  (context, _, paramValues) => WaitUntilAvailable.FromRuntimeValues(context, paramValues) },
    };
}

