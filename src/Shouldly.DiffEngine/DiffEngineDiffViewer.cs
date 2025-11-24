using DiffEngine;

namespace Shouldly;

/// <summary>
/// Implementation of IDiffViewer that uses DiffEngine to compare files
/// </summary>
public class DiffEngineDiffViewer : IDiffViewer
{
    private DiffEngineDiffViewer()
    {
    }

    /// <summary>
    /// Gets the singleton instance of the DiffEngineDiffViewer
    /// </summary>
    public static DiffEngineDiffViewer Instance { get; } = new ();

    /// <inheritdoc/>
    public void Launch(string receivedFile, string approvedFile)
        => DiffRunner.Launch(receivedFile, approvedFile);
}
