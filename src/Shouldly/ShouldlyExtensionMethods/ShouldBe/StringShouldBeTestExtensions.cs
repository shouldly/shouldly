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
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(
        [NotNullIfNotNull(nameof(expected))] this string? actual,
        [NotNullIfNotNull(nameof(actual))] string? expected,
        string? customMessage = null)
    {
        // ReSharper disable once IntroduceOptionalParameters.Global
        ShouldBe(actual, expected, customMessage, 0);
    }

    /// <summary>
    /// Perform a string comparison with sensitivity options
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(
        [NotNullIfNotNull(nameof(expected))] this string? actual,
        [NotNullIfNotNull(nameof(actual))] string? expected,
        StringCompareShould options)
    {
        ShouldBe(actual, expected, (string?)null, options);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(
        [NotNullIfNotNull(nameof(expected))] this string? actual,
        [NotNullIfNotNull(nameof(actual))] string? expected,
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