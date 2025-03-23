using DiffEngine;

namespace Shouldly;

public class DiffEngine : IDiffViewer
{
    private DiffEngine()
    {
    }

    public static DiffEngine Instance { get; } = new DiffEngine();

    public void Launch(string receivedFile, string approvedFile)
        => DiffRunner.Launch(receivedFile, approvedFile);
}
