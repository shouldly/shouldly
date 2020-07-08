using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Shouldly
{
    [ShouldlyMethods]
    public static partial class ObjectGraphTestExtensions
    {
        public static void ShouldBeEquivalentTo(
            [NotNullIfNotNull("expected")] this object? actual,
            [NotNullIfNotNull("actual")] object? expected,
            string? customMessage = null)
        {
            CompareObjects(actual, expected, new List<string>(), new Dictionary<object, IList<object?>>(), customMessage);
        }

        private static void CompareObjects(
            [NotNullIfNotNull("expected")] this object? actual,
            [NotNullIfNotNull("actual")] object? expected,
            IList<string> path,
            IDictionary<object, IList<object?>> previousComparisons,
            string? customMessage,
            [CallerMemberName] string shouldlyMethod = null!)
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
                CompareReferenceTypes(actual, expected, type, path, previousComparisons, customMessage, shouldlyMethod);
            }
        }

        private static bool BothValuesAreNull(
            [NotNullWhen(false)] object? actual,
            [NotNullWhen(false)] object? expected,
            IEnumerable<string> path,
            string? customMessage,
            [CallerMemberName] string shouldlyMethod = null!)
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

        private static Type GetTypeToCompare(object actual, object expected, IList<string> path,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
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
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            if (!actual.Equals(expected))
                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
        }

        private static void CompareReferenceTypes(object actual, object expected, Type type,
            IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            if (ReferenceEquals(actual, expected) ||
                previousComparisons.Contains(actual, expected))
                return;

            previousComparisons.Record(actual, expected);

            if (type == typeof(string))
            {
                CompareStrings((string)actual, (string)expected, path, customMessage, shouldlyMethod);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                CompareEnumerables((IEnumerable)actual, (IEnumerable)expected, path, previousComparisons, customMessage, shouldlyMethod);
            }
            else
            {
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                CompareProperties(actual, expected, properties, path, previousComparisons, customMessage, shouldlyMethod);
            }
        }

        private static void CompareStrings(string actual, string expected, IEnumerable<string> path,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            if (!actual.Equals(expected, StringComparison.Ordinal))
                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
        }

        private static void CompareEnumerables(IEnumerable actual, IEnumerable expected,
            IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            var expectedList = expected.Cast<object?>().ToList();
            var actualList = actual.Cast<object?>().ToList();

            if (actualList.Count != expectedList.Count)
            {
                var newPath = path.Concat(new[] { "Count" });
                ThrowException(actualList.Count, expectedList.Count, newPath, customMessage, shouldlyMethod);
            }

            for (var i = 0; i < actualList.Count; i++)
            {
                var newPath = path.Concat(new[] { $"Element [{i}]" });
                CompareObjects(actualList[i], expectedList[i], newPath.ToList(), previousComparisons, customMessage, shouldlyMethod);
            }
        }

        private static void CompareProperties(object actual, object expected, IEnumerable<PropertyInfo> properties,
            IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            foreach (var property in properties)
            {
                var actualValue = property.GetValue(actual, new object[0]);
                var expectedValue = property.GetValue(expected, new object[0]);

                var newPath = path.Concat(new[] { property.Name });
                CompareObjects(actualValue, expectedValue, newPath.ToList(), previousComparisons, customMessage, shouldlyMethod);
            }
        }

        private static void ThrowException(object? actual, object? expected, IEnumerable<string> path,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            throw new ShouldAssertException(
                new ExpectedEquivalenceShouldlyMessage(expected, actual, path, customMessage, shouldlyMethod).ToString());
        }

        private static bool Contains(this IDictionary<object, IList<object?>> comparisons, object actual, object? expected)
        {
            return comparisons.TryGetValue(actual, out IList<object?>? list)
                   && list.Contains(expected);
        }

        private static void Record(this IDictionary<object, IList<object?>> comparisons, object actual,
            object? expected)
        {
            if (comparisons.TryGetValue(actual, out IList<object?>? list))
                list.Add(expected);
            else
                comparisons.Add(actual, new List<object?>(new[] { expected }));
        }
    }
}
