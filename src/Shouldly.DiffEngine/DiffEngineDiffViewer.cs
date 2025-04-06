using DiffEngine;

namespace Shouldly;

public class DiffEngineDiffViewer : IDiffViewer
{
    private DiffEngineDiffViewer()
    {
    }

    public static DiffEngineDiffViewer Instance { get; } = new DiffEngineDiffViewer();

    public void Launch(string receivedFile, string approvedFile)
        => DiffRunner.Launch(receivedFile, approvedFile);
}
