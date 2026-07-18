using System.ComponentModel;
using Shouldly.Internals.AssertionFactories;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Perform a string comparison with sensitivity options
    /// </summary>
    public static void ShouldBe(
        [NotNullIfNotNull(nameof(expected))] this string? actual,
        [NotNullIfNotNull(nameof(actual))] string? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        // ReSharper disable once IntroduceOptionalParameters.Global
        ShouldBe(actual, expected, customMessage, 0, actualExpression);
    }

    /// <summary>
    /// Perform a string comparison with sensitivity options
    /// </summary>
    public static void ShouldBe(
        [NotNullIfNotNull(nameof(expected))] this string? actual,
        [NotNullIfNotNull(nameof(actual))] string? expected,
        StringCompareShould options,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldBe(actual, expected, null, options, actualExpression);
    }

    /// <summary>
    /// Perform a string comparison with sensitivity options and custom message
    /// </summary>
    public static void ShouldBe(
        [NotNullIfNotNull(nameof(expected))] this string? actual,
        [NotNullIfNotNull(nameof(actual))] string? expected,
        string? customMessage,
        StringCompareShould options,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var assertion = StringShouldBeAssertionFactory.Create(expected, actual, options, actualExpression: actualExpression);
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