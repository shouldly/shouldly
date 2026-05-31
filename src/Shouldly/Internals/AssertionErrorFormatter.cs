namespace Shouldly;

/// <summary>
/// Formats a set of assertion failure messages into the numbered "Error N" block layout
/// used by ShouldSatisfyAllConditions. Each error is followed by a blank line and the block
/// is terminated with a closing divider; dividers are a fixed width so they align in
/// monospaced output regardless of the error number.
/// </summary>
internal static class AssertionErrorFormatter
{
    private const int DividerWidth = 41;

    /// <summary>
    /// Builds the "Error N" blocks for the supplied failure messages.
    /// </summary>
    internal static string FormatErrors(IEnumerable<string?> errorMessages)
    {
        var sb = new StringBuilder();
        var errorCount = 1;
        foreach (var message in errorMessages)
        {
            sb.AppendLine(Divider($"Error {errorCount}"));
            var lines = (message ?? string.Empty).Replace("\r\n", "\n").Split('\n');
            var paddedLines = lines.Select(l => string.IsNullOrEmpty(l) ? l : "    " + l);
            sb.AppendLine(string.Join(Environment.NewLine, paddedLines.ToArray()));
            sb.AppendLine();
            errorCount++;
        }

        sb.AppendLine(Divider());

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Builds a divider line of a fixed width so the error headers and the closing divider
    /// always align in monospaced output, regardless of the error number.
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
