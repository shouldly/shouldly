using System.ComponentModel;
using System.Dynamic;
using JetBrains.Annotations;

namespace Shouldly;

[ShouldlyMethods]
[EditorBrowsable]
public static partial class DynamicShould
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Action actual, string? customMessage = null)
        where TException : Exception =>
        Should.Throw<TException>(actual, customMessage);

    [MethodImpl(MethodImplOptions.NoInlining)]
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