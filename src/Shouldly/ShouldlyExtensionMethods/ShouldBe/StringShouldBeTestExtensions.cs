using Shouldly.Internals.AssertionFactories;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Perform a string comparison with sensitivity options
    /// </summary>
    public static void ShouldBe(
        [NotNullIfNotNull("expected")] this string? actual,
        [NotNullIfNotNull("actual")] string? expected,
        string? customMessage = null)
    {
        // ReSharper disable once IntroduceOptionalParameters.Global
        ShouldBe(actual, expected, customMessage, 0);
    }

    /// <summary>
    /// Perform a string comparison with sensitivity options
    /// </summary>
    public static void ShouldBe(
        [NotNullIfNotNull("expected")] this string? actual,
        [NotNullIfNotNull("actual")] string? expected,
        StringCompareShould options)
    {
        ShouldBe(actual, expected, (string?)null, options);
    }

    public static void ShouldBe(
        [NotNullIfNotNull("expected")] this string? actual,
        [NotNullIfNotNull("actual")] string? expected,
        string? customMessage,
        StringCompareShould options)
    {
        var assertion = StringShouldBeAssertionFactory.Create(expected, actual, options);
        ExecuteAssertion(assertion, customMessage);
    }

    private static void ExecuteAssertion(Internals.Assertions.IAssertion assertion, string? customMessage)
    {
        try
        {
            if (assertion.IsSatisfied()) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(assertion.GenerateMessage(customMessage));
    }
}