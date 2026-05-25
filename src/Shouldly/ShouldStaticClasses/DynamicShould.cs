using System.Dynamic;
using JetBrains.Annotations;
using Shouldly;

namespace Shouldly;

/// <summary>
/// Provides assertion methods for dynamic objects.
/// </summary>
/// <remarks>
/// Members take their target via a <see cref="Func{TResult}"/> lambda
/// (e.g. <c>DynamicShould.HaveProperty(() =&gt; myDynamic, "Foo")</c>) so that the call site
/// remains statically dispatched and
/// <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/> captures
/// the user's expression. Passing a dynamic-typed value directly would trigger runtime dynamic
/// dispatch, which doesn't honour <c>CallerArgumentExpression</c>.
/// </remarks>
[ShouldlyMethods]
public static partial class DynamicShould
{
    /// <summary>
    /// Verifies that the provided action throws an exception of type TException
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Action actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        Should.Throw<TException>(actual, customMessage, actualExpression);

    /// <summary>
    /// Verifies that the dynamic object has the specified property. The dynamic value must be
    /// wrapped in a lambda (<c>() =&gt; myDynamic</c>) so the call site stays statically
    /// dispatched and the caller expression can be captured.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void HaveProperty(
        [InstantHandle] Func<object?> dynamicTestObject,
        string propertyName,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(dynamicTestObject))] string? actualExpression = null)
    {
        var instance = dynamicTestObject();
        var receiverExpression = actualExpression.NormalizeDelegateExpression();

        if (!HasProperty(instance, propertyName))
        {
            throw new ShouldAssertException(new ExpectedShouldlyMessage(propertyName, customMessage, actualExpression: receiverExpression).ToString());
        }
    }

    private static bool HasProperty(object? instance, string propertyName)
    {
        // ExpandoObject (and other property-bag dynamics) — fastest direct check.
        if (instance is IDictionary<string, object> bag)
            return bag.ContainsKey(propertyName);

        // Other IDynamicMetaObjectProvider implementations (e.g. user subclasses of DynamicObject)
        // — ask the meta-object for its dynamic member names rather than assuming IDictionary.
        if (instance is IDynamicMetaObjectProvider provider)
        {
            var metaObject = provider.GetMetaObject(Expression.Constant(provider));
            return metaObject.GetDynamicMemberNames().Contains(propertyName);
        }

        // Plain CLR object — fall through to reflected properties.
        var properties = instance?.GetType().GetProperties() ?? [];
        return properties.Select(x => x.Name).Contains(propertyName);
    }
}