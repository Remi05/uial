using Uial.Contexts.Windows;

using AutomationPropertyIdentifier = System.Int32;

namespace Uial.Assertions
{
    public class PropertyEquals : IAssertion
    {
        public const string Key = "PropertyEquals";

        public string Name => Key;
        private IWindowsVisualContext Context { get; set; }
        private AutomationPropertyIdentifier Property { get; set; }
        private object Value { get; set; }

        public PropertyEquals(IWindowsVisualContext context, AutomationPropertyIdentifier property, object value)
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
