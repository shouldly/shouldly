namespace Shouldly.MessageGenerators;

class DictionaryShouldContainKeyAndValueMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldContainKeyAndValue");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Actual is IDictionary || context.Actual is IEnumerable);
        Debug.Assert(context.Key is object);

        const string format =
            @"{0}
    should contain key
{1}
    with value
{2}
{3}";

        var codePart = context.CodePart;
        var dictionary = context.Actual as IDictionary ?? Convert((IEnumerable)context.Actual);
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

    internal static Dictionary<object, object?> Convert(IEnumerable list)
    {
        var result = new Dictionary<object, object?>();
        PropertyInfo? keyProperty = null;
        PropertyInfo? valueProperty = null;

        foreach (var entry in list)
        {
            var key = (keyProperty ?? entry.GetType().GetProperty("Key"))?.GetValue(entry, null);
            var value = (valueProperty ?? entry.GetType().GetProperty("Value"))?.GetValue(entry, null);
            result.Add(key!, value);
        }

        return result;
    }
}