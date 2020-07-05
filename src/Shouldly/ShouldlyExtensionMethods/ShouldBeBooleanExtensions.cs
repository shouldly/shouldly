using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeBooleanExtensions
    {
        public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool actual)
        {
            ShouldBeTrue(actual, () => null);
        }

        public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool actual, string? customMessage)
        {
            ShouldBeTrue(actual, () => customMessage);
        }

        public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool actual, [InstantHandle] Func<string?>? customMessage)
        {
            if (!actual)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage).ToString());
        }

        public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool actual)
        {
            ShouldBeFalse(actual, () => null);
        }

        public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool actual, string? customMessage)
        {
            ShouldBeFalse(actual, () => customMessage);
        }

        public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool actual, [InstantHandle] Func<string?>? customMessage)
        {
            if (actual)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage).ToString());
        }
    }
}