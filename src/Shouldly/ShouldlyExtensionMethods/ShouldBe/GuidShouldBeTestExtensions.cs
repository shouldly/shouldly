using System.ComponentModel;

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
        actual.AssertAwesomely(v => Is.Equal(Guid.Empty, v), actual, Guid.Empty, customMessage, actualExpression: actualExpression);
    }
}

