using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeDecoratedExtensions
    {
        public static void ShouldBeDecoratedWith<T>(this Type actual) where T : class
        {
            if (actual.HasAttribute(nameof(T)))
                actual.ShouldBeDecoratedWith<FlagsAttribute>();
        }

        public static bool HasAttribute<TAttribute>(this Type type)
        {
            return type.GetCustomAttributes(typeof(TAttribute), true).Any();
        }

        public static bool HasAttribute(this Type type, string attributeName)
        {
            return type.GetCustomAttributes(true).Cast<Attribute>().Any(a => a.GetType().FullName == attributeName);
        }

    }

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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage).ToString());
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage).ToString());
        }
    }
}