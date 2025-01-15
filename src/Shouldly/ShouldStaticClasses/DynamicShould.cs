using System.Dynamic;
using JetBrains.Annotations;

namespace Shouldly;

[ShouldlyMethods]
public static partial class DynamicShould
{
    public static TException Throw<TException>([InstantHandle] Action actual, string? customMessage = null)
        where TException : Exception =>
        Should.Throw<TException>(actual, customMessage);

    public static void HaveProperty(dynamic dynamicTestObject, string propertyName, string? customMessage = null)
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
            var dynamicAsObject = (object)dynamicTestObject;
            var properties = dynamicAsObject.GetType().GetProperties();
            if (!properties.Select(x => x.Name).Contains(propertyName))
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(propertyName, customMessage).ToString());
            }
        }
    }
}