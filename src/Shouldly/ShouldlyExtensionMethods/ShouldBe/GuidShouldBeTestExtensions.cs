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

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeEmpty(this Guid actual, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(Guid.Empty, v), actual, Guid.Empty, customMessage);
    }
}

