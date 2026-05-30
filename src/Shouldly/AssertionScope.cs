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

    private const int DividerWidth = 41;

    private string BuildMessage()
    {
        var sb = new StringBuilder();
        var errorCount = 1;
        var failureCount = failures.Count;
        sb.AppendLine($"{failureCount} assertion{(failureCount == 1 ? "" : "s")} in this scope failed:");
        foreach (var failure in failures)
        {
            if (errorCount > 1)
                sb.AppendLine();
            sb.AppendLine(Divider($"Error {errorCount}"));
            var lines = failure.Message?.Replace("\r\n", "\n").Split('\n') ?? [];
            var paddedLines = lines.Select(l => string.IsNullOrEmpty(l) ? l : "    " + l);
            sb.AppendLine(string.Join(Environment.NewLine, paddedLines.ToArray()));
            errorCount++;
        }

        sb.AppendLine(Divider());

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Builds a divider line of a fixed width so the error headers and the closing
    /// divider always align in monospaced output, regardless of the error number.
    /// </summary>
    private static string Divider(string? label = null)
    {
        if (string.IsNullOrEmpty(label))
            return new string('-', DividerWidth);

        var text = $" {label} ";
        var dashes = Math.Max(DividerWidth - text.Length, 2);
        var left = dashes / 2;
        return new string('-', left) + text + new string('-', dashes - left);
    }
}
