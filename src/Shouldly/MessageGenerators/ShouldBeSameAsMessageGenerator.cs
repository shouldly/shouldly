namespace Shouldly.MessageGenerators;

/// <summary>
/// <c>ShouldBeSameAs</c> fails on identity, and the two values are often equal, so instead of a difference
/// section (which would have nothing to mark) the message says the actual value is a different instance, and
/// whether it's equal by value.
/// </summary>
class ShouldBeSameAsMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod == "ShouldBeSameAs";

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var actualValue = context.Actual.ToStringAwesomely();
        var actual = codePart == actualValue
            ? " not"
            : $"\r\n{actualValue}";

        var message =
            $"""
             {codePart}
                 should be same as
             {context.Expected.ToStringAwesomely()}
                 but was{actual}
             """;

        // With a null on either side there are not two instances to tell apart.
        if (context is { Actual: not null, Expected: not null })
        {
            var equalByValue = AreEqualByValue(context.Actual, context.Expected)
                ? " (the two are equal by value)"
                : string.Empty;
            message += $"""

                            which is a different instance{equalByValue}
                        """;
        }

        return message;
    }

    // The assertion itself never called Equals, so a throwing Equals must not replace the identity failure.
    private static bool AreEqualByValue(object actual, object expected)
    {
        try
        {
            return Is.Equal(actual, expected);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
