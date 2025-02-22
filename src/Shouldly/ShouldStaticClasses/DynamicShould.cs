using System.ComponentModel;
using System.Dynamic;
using JetBrains.Annotations;

namespace Shouldly;

[ShouldlyMethods]
public static partial class DynamicShould
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Action actual, string? customMessage = null)
        where TException : Exception =>
        Should.Throw<TException>(actual, customMessage);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void HaveProperty(dynamic dynamicTestObject, string propertyName, string? customMessage = null)
    {
        if (dynamicTestObject is IDynamicMetaObjectProvider metaProvider)
        {
            // If the test object is an ExpandoObject or delegates property get/set to a dictionary, then the property wouldn't exist on the runtime type.
            if (metaProvider is IDictionary<string, object> dynamicAsDictionary)
            {
                if (!dynamicAsDictionary.ContainsKey(propertyName))
                {
                    throw new ShouldAssertException(new ExpectedShouldlyMessage(propertyName, customMessage).ToString());
                }
                return;
            }
            var meta = metaProvider.GetMetaObject(Expression.Constant(metaProvider));
            var property = meta.RuntimeType?.GetProperty(propertyName);
            if (property is null)
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