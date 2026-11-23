using JetBrains.Annotations;
using System.Diagnostics;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static class ShouldBeFileSystemInfoTestExtensions
{
    public static void ShouldExist([NotNull] this FileInfo actual, string? customMessage = null)
    {
        if (actual is null || !actual.Exists)
        {
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }
    }

    public static void ShouldNotExist([NotNull] this FileInfo actual, string? customMessage = null)
    {
        if (actual is null || actual.Exists)
        {
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }
    }

    public static void ShouldExist([NotNull] this DirectoryInfo actual, string? customMessage = null)
    {
        if (actual is null || !actual.Exists)
        {
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }
    }

    public static void ShouldNotExist([NotNull] this DirectoryInfo actual, string? customMessage = null)
    {
        if (actual is null || actual.Exists)
        {
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }
    }
}

