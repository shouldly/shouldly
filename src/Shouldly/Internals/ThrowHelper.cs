namespace Shouldly;

/// <summary>
/// Single entry point for all assertion failures.
/// Routes to the active <see cref="AssertionScope"/> when one exists,
/// otherwise throws immediately.
/// </summary>
internal static class ThrowHelper
{
    /// <summary>
    /// Throws the assertion exception, or records it if an <see cref="AssertionScope"/> is active.
    /// </summary>
    internal static void ThrowOrRecord(ShouldAssertException exception)
    {
        var scope = AssertionScope.Current;
        if (scope != null)
        {
            scope.Add(exception);
            return;
        }

        throw exception;
    }
}
