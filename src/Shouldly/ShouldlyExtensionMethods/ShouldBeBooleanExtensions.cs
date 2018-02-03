using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeBooleanExtensions
    {
        public static void ShouldBeTrue(this bool actual)
        {
            ShouldBeTrue(actual, () => null);
        }

        public static void ShouldBeTrue(this bool actual, string customMessage)
        {
            ShouldBeTrue(actual, () => customMessage);
        }

        public static void ShouldBeTrue(this bool actual, [InstantHandle]Func<string> customMessage)
        {
            if (!actual)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage));
        }

        public static void ShouldBeTrue(this bool? actual)
        {
            ShouldBeTrue(actual, () => null);
        }

        public static void ShouldBeTrue(this bool? actual, string customMessage)
        {
            ShouldBeTrue(actual, () => customMessage);
        }

        public static void ShouldBeTrue(this bool? actual, [InstantHandle]Func<string> customMessage)
        {
            if (!actual.HasValue || !actual.GetValueOrDefault()) {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage));
            }
        }

        public static void ShouldBeFalse(this bool actual)
        {
            ShouldBeFalse(actual, () => null);
        }

        public static void ShouldBeFalse(this bool actual, string customMessage)
        {
            ShouldBeFalse(actual, () => customMessage);
        }

        public static void ShouldBeFalse(this bool actual, [InstantHandle]Func<string> customMessage)
        {
            if (actual)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage));
        }

        public static void ShouldBeFalse(this bool? actual)
        {
            ShouldBeFalse(actual, () => null);
        }

        public static void ShouldBeFalse(this bool? actual, string customMessage)
        {
            ShouldBeFalse(actual, () => customMessage);
        }

        public static void ShouldBeFalse(this bool? actual, [InstantHandle]Func<string> customMessage)
        {
            if (!actual.HasValue || actual.GetValueOrDefault()) {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage));
            }
        }
    }
}