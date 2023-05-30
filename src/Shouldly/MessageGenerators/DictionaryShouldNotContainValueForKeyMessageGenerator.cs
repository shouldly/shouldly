using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators;

internal class DictionaryShouldNotContainValueForKeyMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new Regex("ShouldNotContainValueForKey");

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return Validator.IsMatch(context.ShouldMethod) && context.Actual is IDictionary;
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Actual is IDictionary);
        Debug.Assert(context.Key is object);

        const string format =
@"{0}
    should not contain key
{1}
    with value
{2}
    {3}";

        var codePart = context.CodePart;
        var dictionary = (IDictionary)context.Actual;
        var keyExists = dictionary.Contains(context.Key);
        var expected = context.Expected.ToStringAwesomely();
        var keyValue = context.Key.ToStringAwesomely();

        if (keyExists)
        {
            return string.Format(format, codePart, keyValue, expected, "but does");
        }

        return string.Format(format, codePart, keyValue, expected, "but the key does not exist");
    }
}

internal class DictionaryShouldNotContainValueForKeyReflectionMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new Regex("ShouldNotContainValueForKey");
    private static readonly Type KeyValuePairType = typeof(KeyValuePair<,>);

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        if (!Validator.IsMatch(context.ShouldMethod))
        {
            return false;
        }

        return context.Actual is not IDictionary && context.Actual is IEnumerable && KeyValuePairType.IsAssignableFrom(GetUnderlyingType(context.Actual.GetType()));
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Actual is not IDictionary && context.Actual is IEnumerable && KeyValuePairType.IsAssignableFrom(GetUnderlyingType(context.Actual.GetType())));
        Debug.Assert(context.Key is object);

        const string format =
@"{0}
    should not contain key
{1}
    with value
{2}
    {3}";

        var codePart = context.CodePart;
        var enumerable = (IEnumerable)context.Actual;

        var keyExists = false;
        foreach (var kvp in enumerable)
        {
            var property = kvp.GetType().GetProperty("Key");
            if (property is null) continue;

            var key = property.GetValue(kvp, null);
            keyExists |= Equals(key, context.Key);
        }

        var expected = context.Expected.ToStringAwesomely();
        var keyValue = context.Key.ToStringAwesomely();

        if (keyExists)
        {
            return string.Format(format, codePart, keyValue, expected, "but does");
        }

        return string.Format(format, codePart, keyValue, expected, "but the key does not exist");
    }

    private Type GetUnderlyingType(Type type)
    {
        if (type.IsArray && type.GetElementType() is { } elementType)
        {
            return elementType.GetGenericTypeDefinition();
        }

        if (type.IsGenericType)
        {
            return type.GetGenericArguments()[0].GetGenericTypeDefinition();
        }

        throw new InvalidOperationException("Provided type is not as expected.");
    }
}