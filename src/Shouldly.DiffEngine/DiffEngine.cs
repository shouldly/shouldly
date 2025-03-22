using DiffEngine;

namespace Shouldly;

public class DiffEngine : IDiffEngine
{
    private DiffEngine()
    {
    }

    public static DiffEngine Instance { get; } = new DiffEngine();

    public void Launch(string receivedFile, string approvedFile)
        => DiffRunner.Launch(receivedFile, approvedFile);
}
