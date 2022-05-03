using System;
using System.Collections.Generic;
using System.Reflection;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;

namespace Uial.Modules
{
    public class ModuleProvider : IModuleProvider
    {
        public Module GetModule(ModuleDefinition moduleDefinition)
        {
            var assembly = Assembly.LoadFile(moduleDefinition.BinaryPath);
            var interactionProviders = GetInteractionProviders(assembly);
            var contextProviders = GetContextProviders(assembly);
            return new Module(moduleDefinition.ModuleName, interactionProviders, contextProviders);
        }

        protected ICollection<IInteractionProvider> GetInteractionProviders(Assembly assembly)
        {
            var interactionProviders = new List<IInteractionProvider>();
            var types = assembly.GetExportedTypes();

            foreach (Type type in types)
            {
                bool isIInteractionProvider = type.GetInterface(nameof(IInteractionProvider)) != null;
                if (isIInteractionProvider)
                {
                    IInteractionProvider interactionProvider = GetInteractionProviderInstance(assembly, type);
                    interactionProviders.Add(interactionProvider);
                }
            }

            return interactionProviders;
        }

        protected ICollection<IContextProvider> GetContextProviders(Assembly assembly)
        {
            var contextProviders = new List<IContextProvider>();
            var types = assembly.GetExportedTypes();

            foreach (Type type in types)
            {
                bool isIContextProvider = type.GetInterface(nameof(IContextProvider)) != null;
                if (isIContextProvider)
                {
                    IContextProvider contextProvider = GetContextProviderInstance(assembly, type);
                    contextProviders.Add(contextProvider);
                }
            }

            return contextProviders;
        }

        protected IInteractionProvider GetInteractionProviderInstance(Assembly assembly, Type interactionProviderType)
        {
            object interactionProviderObject = Activator.CreateInstance(interactionProviderType);
            if (interactionProviderObject == null)
            {
                throw new Exception($"Failed to create instance of type {interactionProviderType.Name}.");
            }

            if (!(interactionProviderObject is IInteractionProvider))
            {
                throw new Exception($"{interactionProviderType.Name} does not inherit from {nameof(IInteractionProvider)}");
            }

            return (IInteractionProvider)interactionProviderObject;
        }

        protected IContextProvider GetContextProviderInstance(Assembly assembly, Type contextProviderType)
        {
            object contextProviderObject = Activator.CreateInstance(contextProviderType);
            if (contextProviderObject == null)
            {
                throw new Exception($"Failed to create instance of type {contextProviderType.Name}.");
            }

            if (!(contextProviderObject is IContextProvider))
            {
                throw new Exception($"{contextProviderType.Name} does not inherit from {nameof(IContextProvider)}");
            }

            return (IContextProvider)contextProviderObject;
        }
    }
}
