﻿using Uial.Scopes;

namespace Uial.Contexts
{
    public interface IContext
    {
        RuntimeScope Scope { get; }

        string Name { get; }

        bool IsAvailable();
    }
}
