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
    public static DiffEngineDiffViewer Instance { get; } = new DiffEngineDiffViewer();

    /// <summary>
    /// Launches the diff viewer to compare two files
    /// </summary>
    /// <param name="receivedFile">The path to the received file</param>
    /// <param name="approvedFile">The path to the approved file</param>
    public void Launch(string receivedFile, string approvedFile)
        => DiffRunner.Launch(receivedFile, approvedFile);
}
