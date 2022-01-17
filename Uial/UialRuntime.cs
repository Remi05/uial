using System;
using System.Collections.Generic;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Modules;
using Uial.Scopes;
using Uial.Testing;

namespace Uial
{
    public class UialRuntime
    {
        protected List<IInteractionProvider> InteractionProviders { get; set; }
        protected GlobalContextProvider ContextProvider { get; set; }
        protected GlobalInteractionProvider InteractionProvider { get; set; }
        protected IModuleProvider ModuleProvider { get; set; }

        protected IBaseContextResolver BaseContextResolver { get; set; }
        protected IContextResolver ContextResolver { get; set; }
        protected IBaseInteractionResolver BaseInteractionResolver { get; set; }
        protected IInteractionResolver InteractionResolver { get; set; }
        protected IValueResolver ValueResolver { get; set; }
        protected ITestableResolver TestableResolver { get; set; }

        protected Script RootScript { get; set; } = new Script();
        public RuntimeScope RootScope { get; protected set; }
        public IContext RootContext { get; protected set; }

        public UialRuntime(IContext rootContext)
        {

        }

        public void Initialize()
        {
            ContextProvider = new GlobalContextProvider();
            InteractionProvider = new GlobalInteractionProvider();
            ModuleProvider = new ModuleProvider();

            ValueResolver = new ValueResolver();
            ContextResolver = new ContextResolver();
            BaseContextResolver = new BaseContextResolver(ContextProvider, ValueResolver);
            BaseInteractionResolver = new BaseInteractionResolver(InteractionProvider, BaseContextResolver, ValueResolver);
            InteractionResolver = new InteractionResolver(BaseInteractionResolver);
            TestableResolver = new TestableResolver(BaseInteractionResolver);

            var scopeContextProvider = new ScopedContextProvider(ContextResolver);
            ContextProvider.AddProvider(scopeContextProvider);

            var scopeInteractionProvider = new ScopedInteractionProvider(InteractionResolver);
            InteractionProvider.AddProvider(scopeInteractionProvider);
        }

        public void AddScript(Script script)
        {
            RootScript.AddScript(script);
            // TODO: Merge root scopes 
            // TODO: Add ScenarioDefinitions, update providers
            // TODO: Add TestDefinitions, update providers
            foreach (ModuleDefinition moduleDefinition in script.ModuleDefinitions.Values)
            {
                AddModule(moduleDefinition);
            }
        }

        public void AddModule(ModuleDefinition moduleDefinition)
        {
            Module module = ModuleProvider.GetModule(moduleDefinition);
            InteractionProvider.AddMultipleProviders(module.InteractionProviders);
        }

        public void AddContextProvider(IContextProvider contextProvider)
        {
            ContextProvider.AddProvider(contextProvider);
        }

        public void AddInteractionProvider(IInteractionProvider interactionProvider)
        {
            InteractionProvider.AddProvider(interactionProvider);
        }

        public void RunScenario(string scenarioName)
        {
            if (!RootScript.ScenarioDefinitions.ContainsKey(scenarioName))
            {
                throw new ArgumentException($"Scenario \"{scenarioName}\" could not be found.");
            }
            IInteraction scenario = InteractionResolver.Resolve(RootScript.ScenarioDefinitions[scenarioName], null, RootContext);
            scenario.Do();
        }

        public ITestResults RunTest(string testName)
        {
            if (!RootScript.TestDefinitions.ContainsKey(testName))
            {
                throw new ArgumentException($"Test \"{testName}\" could not be found.");
            }
            ITestable test = TestableResolver.Resolve(RootScript.TestDefinitions[testName], RootContext, null); // TODO: Add value store
            return test.RunTest();
        }
    }
}
