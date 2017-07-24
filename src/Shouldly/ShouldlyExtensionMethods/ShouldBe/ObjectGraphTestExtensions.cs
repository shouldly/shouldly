using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (BothValuesAreNull(actual, expected, path, customMessage, shouldlyMethod))
                return;

            var type = GetTypeToCompare(actual, expected, path, customMessage, shouldlyMethod);

#if NewReflection
            if (type.GetTypeInfo().IsValueType)
#else
            if (type.IsValueType)
#endif
            {
                CompareValueTypes((ValueType)actual, (ValueType)expected, path, customMessage, shouldlyMethod);
            }
            else
            {
                CompareReferenceTypes(actual, expected, type, path, customMessage, shouldlyMethod);
            }
        }

        private static Type GetTypeToCompare(object actual, object expected, IList<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            var expectedType = expected.GetType();
            var actualType = actual.GetType();

            if (actualType != expectedType)
                ThrowException(actualType, expectedType, path, customMessage, shouldlyMethod);

            var typeName = $" [{actualType.FullName}]";
            if (path.Count == 0)
                path.Add(typeName);
            else
                path[path.Count - 1] += typeName;

            return actualType;
        }

        private static void CompareValueTypes(ValueType actual, ValueType expected, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            if (!actual.Equals(expected))
                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
        }

        private static void CompareReferenceTypes(object actual, object expected, Type type, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            if (ReferenceEquals(actual, expected))
                return;

            if (type == typeof(string))
            {
                CompareStrings((string)actual, (string)expected, path, customMessage, shouldlyMethod);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                CompareEnumerables((IEnumerable)actual, (IEnumerable)expected, path, customMessage, shouldlyMethod);
            }
        }

        private static bool BothValuesAreNull(object actual, object expected, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            if (expected == null)
            {
                if (actual == null)
                    return true;

                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
            }
            else if (actual == null)
            {
                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
            }

            return false;
        }

        private static void CompareStrings(string actual, string expected, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            if (!actual.Equals(expected, StringComparison.Ordinal))
                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
        }

        private static void CompareEnumerables(IEnumerable actual, IEnumerable expected, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            var expectedList = expected.Cast<object>().ToList();
            var actualList = actual.Cast<object>().ToList();

            if (actualList.Count != expectedList.Count)
                ThrowException(actualList.Count, expectedList.Count, path.Concat(new[] { "Count" }), customMessage, shouldlyMethod);

            for (var i = 0; i < actualList.Count; i++)
            {
                CompareObjects(actualList[i], expectedList[i], path.Concat(new[] { $"Element [{i}]" }).ToList(), customMessage, shouldlyMethod);
            }
        }

        private static void ThrowException(object actual, object expected, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            throw new ShouldAssertException(
                new ExpectedEquvalenceShouldlyMessage(expected, actual, path, customMessage, shouldlyMethod).ToString());
        }
    }
}
