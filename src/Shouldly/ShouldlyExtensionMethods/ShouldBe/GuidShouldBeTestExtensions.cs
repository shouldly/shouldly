using System.ComponentModel;
using Shouldly.Internals.Failures;

namespace Shouldly;

/// <summary>
/// Extension methods for Guid assertions
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class GuidShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that a Guid is equal to <see cref="Guid.Empty"/>
    /// </summary>
    public static void ShouldBeEmpty(this Guid actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => Is.Equal(v, Guid.Empty)))
            throw Fail.ShouldBeEmpty.ComparedWith(Guid.Empty).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a Guid is not equal to <see cref="Guid.Empty"/>
    /// </summary>
    public static void ShouldNotBeEmpty(this Guid actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => !Is.Equal(v, Guid.Empty)))
            throw Fail.ShouldNotBeEmpty.ComparedWith(Guid.Empty).For(actual, customMessage, actualExpression);
    }
}

