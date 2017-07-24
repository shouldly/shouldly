using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ObjectGraphTestExtensions
    {
        public static void ShouldBeEquivalentTo(this object actual, object expected)
        {
            ShouldBeEquivalentTo(actual, expected, () => null);
        }

        public static void ShouldBeEquivalentTo(this object actual, object expected, string customMessage)
        {
            ShouldBeEquivalentTo(actual, expected, () => customMessage);
        }

        public static void ShouldBeEquivalentTo(this object actual, object expected, [InstantHandle] Func<string> customMessage)
        {
            CompareObjects(actual, expected, new List<string>(), customMessage);
        }

        private static void CompareObjects(object actual, object expected, IList<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            var type = GetTypeToCompare(actual, expected, path, customMessage, shouldlyMethod);
        }

        private static Type GetTypeToCompare(object actual, object expected, IList<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            var expectedType = expected.GetType();
            var actualType = actual.GetType();

            if (actualType != expectedType)
                ThrowException(actualType, expectedType, path, customMessage, shouldlyMethod);

            return actualType;
        }

        private static void ThrowException(object actual, object expected, IList<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            throw new ShouldAssertException(
                new ExpectedEquvalenceShouldlyMessage(expected, actual, path, customMessage, shouldlyMethod).ToString());
        }
    }
}
