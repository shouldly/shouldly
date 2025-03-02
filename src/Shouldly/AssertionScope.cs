namespace Shouldly;

public class AssertionScope : IDisposable
{
    private static readonly AsyncLocal<AssertionScope?> currentScope = new AsyncLocal<AssertionScope?>();

    private readonly List<string> errors = new List<string>();

    public AssertionScope() =>
        // Set this instance as the current scope using AsyncLocal.
        currentScope.Value = this;

    // This method is called by assertion extensions when a failure occurs.
    public static void AddFailure(string message)
    {
        if (currentScope.Value != null)
        {
            currentScope.Value.errors.Add(message);
        }
        else
        {
            // No active scope: throw immediately.
            throw new ShouldAssertException(message);
        }
    }

    public void Dispose()
    {
        // Clear the current scope.
        currentScope.Value = null;

        if (errors.Count > 0)
        {
            throw new AggregateShouldlyAssertionException(errors);
        }
    }
}