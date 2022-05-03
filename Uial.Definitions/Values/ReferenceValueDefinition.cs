using System;

namespace Uial.DataModels
{
    public class ReferenceValueDefinition : ValueDefinition
    {
        public string ReferenceName { get; protected set; }

        public ReferenceValueDefinition(string referenceName) 
        {
            if (referenceName == null)
            {
                throw new ArgumentNullException(nameof(referenceName));
            }
            if (string.IsNullOrWhiteSpace(referenceName))
            {
                throw new ArgumentException($"{nameof(referenceName)} cannot be empty or white space.");
            }
            ReferenceName = referenceName;
        }

        public override string ToString()
        {
            return ReferenceName?.ToString();
        }
    }
}
