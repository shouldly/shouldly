using System.Collections.Concurrent;

namespace Shouldly;

/// <summary>
/// Collects assertion failures and reports them all at once when disposed.
/// Nested scopes propagate failures to the outermost scope.
/// </summary>
/// <remarks>
/// Because failures are recorded rather than thrown while a scope is active, assertions cannot
/// short-circuit. Assertions that normally return a value (such as <c>ShouldNotBeNull</c> or
/// <c>Should.Throw&lt;T&gt;</c>) return <see langword="null"/>/<see langword="default"/> when they
/// fail inside a scope, and nullability/contract annotations (for example <c>[NotNull]</c>) no
/// longer hold. Guard such results defensively when chaining off them inside a scope.
/// </remarks>
public sealed class AssertionScope : IDisposable
{
    private static readonly AsyncLocal<AssertionScope?> currentScope = new();

    private readonly AssertionScope? parent;
    private readonly ConcurrentQueue<ShouldAssertException> failures = new();
    private bool disposed;

    /// <summary>
    /// Creates a new assertion scope. If a parent scope exists, this becomes a nested child.
    /// </summary>
    public AssertionScope()
    {
        parent = currentScope.Value;
        currentScope.Value = this;
    }

    /// <summary>
    /// Gets the currently active assertion scope, or <see langword="null"/> if none is active.
    /// </summary>
    internal static AssertionScope? Current => currentScope.Value;

    internal void Add(ShouldAssertException exception) =>
        failures.Enqueue(exception);

    /// <summary>
    /// Disposes the scope. If this is the outermost scope and failures were collected,
    /// throws a <see cref="ShouldAssertException"/> containing all failure messages.
    /// Nested scope failures are propagated to the parent scope.
    /// </summary>
    public void Dispose()
    {
        if (disposed)
            return;

        disposed = true;
        currentScope.Value = parent;

        if (parent != null)
        {
            foreach (var failure in failures)
            {
                parent.Add(failure);
            }
        }
        else if (!failures.IsEmpty)
        {
            throw new ShouldAssertException(BuildMessage());
        }
    }

    private string BuildMessage()
    {
        var failureCount = failures.Count;
        var header = $"{failureCount} assertion{(failureCount == 1 ? "" : "s")} in this scope failed:";
        return header + Environment.NewLine + AssertionErrorFormatter.FormatErrors(failures.Select(f => f.Message));
    }
}
