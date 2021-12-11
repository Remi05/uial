using System.Collections.Generic;

namespace Uial.Values
{
    public class ReferenceValueStore : IReferenceValueStore
    {
        protected IDictionary<string, object> Values { get; set; } = new Dictionary<string, object>();

        public bool IsKnownReferenceValue(string referenceName)
        {
            return Values.ContainsKey(referenceName);
        }

        public object GetValue(string referenceName)
        {
            if (!IsKnownReferenceValue(referenceName))
            {
                throw new ReferenceValueNotFoundException(referenceName);
            }
            return Values[referenceName];
        }

        public void SetValue(string referenceName, object value)
        {
            Values[referenceName] = value;
        }

        public IReferenceValueStore GetCopy()
        {
            var newValueScope = new ReferenceValueStore();
            newValueScope.Values = new Dictionary<string, object>(Values);
            return newValueScope;
        }

        public IReferenceValueStore WithValues(IDictionary<string, object> newValues)
        {
            var newValueScope = GetCopy();
            if (newValues != null)
            {
                foreach (string key in newValues.Keys)
                {
                    newValueScope.SetValue(key, newValues[key]);
                }
            }
            return newValueScope;
        }
    }
}
