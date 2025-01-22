namespace Shouldly.MessageGenerators;

class DictionaryShouldNotContainValueForKeyMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldNotContainValueForKey");

    public override bool CanProcess(IShouldlyAssertionContext context) => Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Actual is IDictionary || context.Actual is IEnumerable);
        Debug.Assert(context.Key is object);

        const string format =
            """
            {0}
                should not contain key
            {1}
                with value
            {2}
                {3}
            """;

        var codePart = context.CodePart;
        var dictionary = context.Actual as IDictionary ?? DictionaryShouldContainKeyAndValueMessageGenerator.Convert((IEnumerable)context.Actual);
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