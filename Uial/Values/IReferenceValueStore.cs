using System.Collections.Generic;

namespace Uial.Values
{
    public interface IReferenceValueStore
    {
        bool IsKnownReferenceValue(string referenceName);
        object GetValue(string referenceName);
        void SetValue(string referenceName, object value);
        IReferenceValueStore GetCopy();
    }
}