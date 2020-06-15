using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Conditions
{
    public interface IConditionDefinition
    {
        Condition Resolve(RuntimeScope scope);
    }
}
