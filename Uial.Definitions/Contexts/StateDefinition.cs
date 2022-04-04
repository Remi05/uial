
namespace Uial.DataModels
{
    public class StateDefinition
    {
        public string Name { get; protected set; }
        public ConditionDefinition Condition { get; protected set; }

        public StateDefinition(string name, ConditionDefinition condition)
        {
            Name = name;
            Condition = condition;
        }
    }
}
