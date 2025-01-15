using System.Runtime.CompilerServices;
using static Shouldly.Tests.CommonWaitDurations;

namespace Shouldly.Tests;

internal static class ModuleInitializer
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        ShouldlyConfiguration.DefaultTaskTimeout = LongWait;
    }
}