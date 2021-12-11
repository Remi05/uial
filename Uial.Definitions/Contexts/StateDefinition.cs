
namespace Uial.DataModels
{
    public class StateDefinition
    {
        public string Name { get; private set; }
        public ConditionDefinition Condition { get; private set; }

        public StateDefinition(string name, ConditionDefinition condition)
        {
            Name = name;
            Condition = condition;
        }
    }
}
