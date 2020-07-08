using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeBooleanExtensions
    {
        public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool actual, string? customMessage = null)
        {
            if (!actual)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage).ToString());
        }

        public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool actual, string? customMessage = null)
        {
            if (actual)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage).ToString());
        }
    }
}