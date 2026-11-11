using System.ComponentModel;
using Shouldly.Internals;
using Shouldly.Internals.Failures;

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
        var strings = ComparedStrings.Under(options, actual, expected, useEqualityRules: true);
        if (!strings.Satisfies(compared => compared.AreEqual()))
            throw Fail.ShouldBe.With(strings).For(strings.Actual, customMessage, actualExpression);
    }
}