using System;

namespace Shouldly
{
    [Flags]
    public enum ShouldBeStringOptions
    {
        None = 0,
        IgnoreCase = 1,
        IgnoreLineEndings = 2
    }
}
