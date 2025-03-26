using System.ComponentModel;

namespace Shouldly;

[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class GuidShouldBeTestExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeEmpty(this Guid actual, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Equal(Guid.Empty, v), actual, Guid.Empty, customMessage);
    }

    /// <summary>
    /// Asserts that a Guid is not equal to <see cref="Guid.Empty"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeEmpty(this Guid actual, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(Guid.Empty, v), actual, Guid.Empty, customMessage);
    }
}

