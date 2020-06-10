using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class LaunchApp : IInteraction
    {
        public string Name => "LaunchApp";

        public string AppId { get; set; }

        public LaunchApp(string appId)
        {
            AppId = appId;
        }

        public void Do(IContext context)
        {
            
        }

        public bool IsAvailable(IContext context)
        {
            return true;
        }

    }
}
