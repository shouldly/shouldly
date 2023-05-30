using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators;

internal class DictionaryShouldContainKeyAndValueMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new Regex("ShouldContainKeyAndValue");

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
    should contain key
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
            var actualValueString = dictionary[context.Key].ToStringAwesomely();
            var valueString =
$@"    but value was
{actualValueString}";
            return string.Format(format, codePart, keyValue, expected, valueString);
        }

        return string.Format(format, codePart, keyValue, expected, "    but the key does not exist");
    }
}

internal class DictionaryShouldContainKeyAndValueReflectionMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new Regex("ShouldContainKeyAndValue");
    private static readonly Type KeyValuePairType = typeof(KeyValuePair<,>);

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return Validator.IsMatch(context.ShouldMethod) && context.Actual is IEnumerable && KeyValuePairType.IsAssignableFrom(GetUnderlyingType(context.Actual.GetType()));
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Actual is IEnumerable && KeyValuePairType.IsAssignableFrom(GetUnderlyingType(context.Actual.GetType())));
        Debug.Assert(context.Key is object);

        const string format =
@"{0}
    should contain key
{1}
    with value
{2}
{3}";

        var codePart = context.CodePart;
        var enumerable = (IEnumerable)context.Actual;
        var expected = context.Expected.ToStringAwesomely();
        var keyValue = context.Key.ToStringAwesomely();

        foreach (var kvp in enumerable)
        {
            var keyProperty = kvp.GetType().GetProperty("Key")!;

            var key = keyProperty.GetValue(kvp, null);
            if (Equals(key, context.Key))
            {
                var valueProperty = kvp.GetType().GetProperty("Value")!;
                var value = valueProperty.GetValue(kvp, null);
                var actualValueString = value.ToStringAwesomely();
                var valueString =
$@"    but value was
{actualValueString}";
                return string.Format(format, codePart, keyValue, expected, valueString);
            }
        }

        return string.Format(format, codePart, keyValue, expected, "    but the key does not exist");
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