using System;
using System.Collections.Generic;
using System.Reflection;
using Uial.Interactions;

namespace Uial.Modules
{
    public class ModuleProvider : IModuleProvider
    {
        public Module GetModule(ModuleDefinition moduleDefinition)
        {
            var assembly = Assembly.LoadFile(moduleDefinition.BinaryPath);
            var interactionProviders = GetInteractionProviders(assembly);
            return new Module(moduleDefinition.Name, interactionProviders);
        }

        protected IEnumerable<IInteractionProvider> GetInteractionProviders(Assembly assembly)
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
    }
}
