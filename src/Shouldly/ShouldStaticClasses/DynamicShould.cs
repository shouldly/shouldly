using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using JetBrains.Annotations;

#if NewReflection
using System.Reflection;
#endif

namespace Shouldly
{
    [ShouldlyMethods]
    public static class DynamicShould
    {
        public static void HaveProperty(dynamic dynamicTestObject, string propertyName)
        {
            Func<string> customMessage = () => null;
            HaveProperty(dynamicTestObject, propertyName, customMessage);
        }

        public static void HaveProperty(dynamic dynamicTestObject, string propertyName, string customMessage)
        {
            Func<string> message = () => customMessage;
            HaveProperty(dynamicTestObject, propertyName, message);
        }

        public static void HaveProperty(dynamic dynamicTestObject, string propertyName, [InstantHandle] Func<string> customMessage)
        {
            if (dynamicTestObject is IDynamicMetaObjectProvider)
            {
                var dynamicAsDictionary = (IDictionary<string, object>)dynamicTestObject;

                if (!dynamicAsDictionary.ContainsKey(propertyName))
                {
                    throw new ShouldAssertException(new ExpectedShouldlyMessage(propertyName, customMessage).ToString());
                }
            }
            else
            {
                /*
                 * TODO: Fix
                 * var dynamicAsObject = (object)dynamicTestObject;
                var properties = dynamicAsObject.GetType().GetTypeInfo().DeclaredProperties;
                if (!properties.Select(x => x.Name).Contains(propertyName))
                {
                    throw new ShouldAssertException(new ExpectedShouldlyMessage(propertyName, customMessage).ToString());
                }*/
            }
        }
    }
}