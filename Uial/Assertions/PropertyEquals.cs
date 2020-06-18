using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Assertions
{
    public class PropertyEquals : IAssertion
    {
        public const string Key = "PropertyEquals";

        public string Name => Key;
        private IContext Context { get; set; }
        private AutomationProperty Property { get; set; }
        private object Value { get; set; }

        public PropertyEquals(IContext context, AutomationProperty property, object value)
        {
            Context = context;
            Property = property;
            Value = value;
        }

        public bool Assert()
        {
            if (Context?.RootElement == null)
            {
                return false;
            }
            object actualValue = Context.RootElement.GetCurrentPropertyValue(Property);
            return Value == null && actualValue == null
                || Value != null && Value.Equals(actualValue);
        }
    }
}
