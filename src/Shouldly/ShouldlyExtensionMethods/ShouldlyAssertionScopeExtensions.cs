namespace Shouldly.ShouldlyExtensionMethods;

public static class ShouldlyAssertionScopeExtensions
{
    /// <summary>
    /// Executes an assertion on the current object and captures any assertion failure in the active AssertionScope.
    /// </summary>
    /// <typeparam name="T">Type of the object to assert on.</typeparam>
    /// <param name="actual">The actual value to be asserted.</param>
    /// <param name="assertion">A lambda that performs the assertion (e.g. x => x.ShouldBe(expected)).</param>
    public static void ShouldWithScope<T>(this T actual, Action<T> assertion)
    {
        try
        {
            assertion(actual);
        }
        catch (ShouldAssertException ex)
        {
            // Capture the assertion failure in the active scope.
            AssertionScope.AddFailure(ex.Message);
        }
    }
}