﻿using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions
{
    public interface IBaseInteractionDefinition
    {
        IInteraction Resolve(IContext parentContext, IInteractionProvider interactionProvider, RuntimeScope currentScope);
    }
}
