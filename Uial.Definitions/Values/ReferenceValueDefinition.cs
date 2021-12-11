using System;

namespace Uial.DataModels
{
    public class ReferenceValueDefinition : ValueDefinition
    {
        public string ReferenceName { get; private set; }

        public ReferenceValueDefinition(string referenceName) 
        {
            if (referenceName == null)
            {
                throw new ArgumentNullException(nameof(referenceName));
            }
            ReferenceName = referenceName;
        }

        public override string ToString()
        {
            return ReferenceName?.ToString();
        }
    }
}
