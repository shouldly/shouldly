namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class ShouldBeDecoratedWithExtensions
{
    public static void ShouldBeDecoratedWith<T>(this Type actual, string? customMessage = null)
        where T : Attribute
    {
        if (!actual.HasAttribute(typeof(T)))
            throw new ShouldAssertException(new ExpectedShouldlyMessage(typeof(T).Name, customMessage).ToString());
    }

    private static bool HasAttribute(this Type type, Type attributeType) =>
        type.GetCustomAttributes(attributeType, true).Any();
}