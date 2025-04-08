namespace Shouldly;

/// <summary>
/// Interface for diff viewers that can compare files
/// </summary>
public interface IDiffViewer
{
    /// <summary>
    /// Launches the diff viewer to compare two files
    /// </summary>
    /// <param name="receivedFile">The path to the received file</param>
    /// <param name="approvedFile">The path to the approved file</param>
    void Launch(string receivedFile, string approvedFile);
}
