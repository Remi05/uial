using System;
using System.Collections.Generic;
using UIAutomationClient;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Windows.Conditions
{
    public class ConditionResolver : IConditionResolver
    {
        protected IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        protected IValueResolver ValueResolver { get; set; }

        public ConditionResolver(IValueResolver valueResolver)
        {
            ValueResolver = valueResolver;
        }

        public IUIAutomationCondition Resolve(ConditionDefinition conditionDefinition, IReferenceValueStore referenceValueStore)
        {
            if (conditionDefinition == null)
            {
                throw new ArgumentNullException(nameof(conditionDefinition));
            }
            if (conditionDefinition is PropertyConditionDefinition)
            {
                return ResolvePropertyCondition(conditionDefinition as PropertyConditionDefinition, referenceValueStore);
            }
            else if (conditionDefinition is CompositeConditionDefinition)
            {
                return ResolveCompositeCondition(conditionDefinition as CompositeConditionDefinition, referenceValueStore);
            }
            // TODO: throw InvalidConditionDefinitionTypeException
            return null;
        }

        protected IUIAutomationCondition ResolvePropertyCondition(PropertyConditionDefinition propertyConditionDefinition, IReferenceValueStore referenceValueStore)
        {
            var propertyIdentifier = Properties.GetPropertyByName(propertyConditionDefinition.PropertyName);
            object propertyValue = ValueResolver.Resolve(propertyConditionDefinition.Value, referenceValueStore);
            propertyValue = Properties.GetPropertyValue(propertyIdentifier, propertyValue);
            return UIAutomation.CreatePropertyCondition(propertyIdentifier, propertyValue);
        }

        protected IUIAutomationCondition ResolveCompositeCondition(CompositeConditionDefinition compositeConditionDefinition, IReferenceValueStore referenceValueStore)
        {
            var automationConditions = new List<IUIAutomationCondition>();
            foreach (var condition in compositeConditionDefinition.Conditions)
            {
                var automationCondition = Resolve(condition, referenceValueStore);
                if (automationCondition != null)
                {
                    automationConditions.Add(automationCondition);
                }
            }
            return UIAutomation.CreateAndConditionFromArray(automationConditions.ToArray());
        }
    }
}
