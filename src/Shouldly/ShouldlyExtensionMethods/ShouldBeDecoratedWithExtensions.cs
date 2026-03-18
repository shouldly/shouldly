using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for attribute decoration assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeDecoratedWithExtensions
{
    /// <summary>
    /// Asserts that the type is decorated with the specified attribute.
    /// </summary>
    public static void ShouldBeDecoratedWith<T>(this Type actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : Attribute
    {
        if (!actual.HasAttribute(typeof(T)))
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(typeof(T).Name, customMessage, actualExpression: actualExpression).ToString()));
    }

    private static bool HasAttribute(this Type type, Type attributeType) =>
        type.GetCustomAttributes(attributeType, true).Any();
}