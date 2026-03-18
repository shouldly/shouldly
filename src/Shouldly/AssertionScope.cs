using System.Collections.Concurrent;

namespace Shouldly;

/// <summary>
/// Collects assertion failures and reports them all at once when disposed.
/// Nested scopes propagate failures to the outermost scope.
/// </summary>
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
        var sb = new StringBuilder();
        var errorCount = 1;
        sb.AppendLine();
        foreach (var failure in failures)
        {
            sb.AppendLine($"--------------- Error {errorCount} ---------------");
            var lines = failure.Message?.Replace("\r\n", "\n").Split('\n') ?? [];
            var paddedLines = lines.Select(l => string.IsNullOrEmpty(l) ? l : "    " + l);
            sb.AppendLine(string.Join("\r\n", paddedLines.ToArray()));
            sb.AppendLine();
            errorCount++;
        }

        sb.AppendLine("-----------------------------------------");

        return sb.ToString().TrimEnd();
    }
}
