using System;

namespace Uial.Windows.Conditions
{
    public class UnknownPropertyException : Exception
    {
        private string PropertyName { get; set; }

        public override string Message => $"No property named {PropertyName} could be found.";

        public UnknownPropertyException(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
