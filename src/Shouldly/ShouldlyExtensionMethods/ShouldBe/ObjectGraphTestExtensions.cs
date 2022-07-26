using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Shouldly
{
    [ShouldlyMethods]
    public static partial class ObjectGraphTestExtensions
    {
        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.Instance;

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

            if (type.GetTypeInfo().IsValueType)
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
            IList<string> path, IDictionary<object, IList<object?>> previousComparisons,
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
                CompareEnumerables((IEnumerable)actual, (IEnumerable)expected, path, customMessage, shouldlyMethod);
            }
            else
            {
                var fields = type.GetFields(DefaultBindingFlags);
                CompareFields(actual, expected, fields, path, previousComparisons, customMessage, shouldlyMethod);

                var properties = type.GetProperties(DefaultBindingFlags);
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
            IEnumerable<string> path, string? customMessage,
            [CallerMemberName] string shouldlyMethod = null!)
        {
            var expectedList = expected.Cast<object?>().ToList();
            var actualList = actual.Cast<object?>().ToList();

            if (actualList.Count != expectedList.Count)
            {
                var newPath = path.Concat(new[] { "Count" });
                ThrowException(actualList.Count, expectedList.Count, newPath, customMessage, shouldlyMethod);
            }

            if (expectedList.Count != 0 && FindMismatchedElement(
                expectedList,
                actualList,
                out _,
                out _,
                out var mismatchedElement))
            {
                var newPath = path.Concat(new[] { $"Element {mismatchedElement} not found" });

                throw new ShouldAssertException(
                    new ExpectedEquivalenceShouldlyMessage(
                        expected,
                        actual,
                        newPath,
                        customMessage,
                        shouldlyMethod).ToString());
            }
        }

        private static void CompareFields(object actual, object expected, IEnumerable<FieldInfo> fields,
            IList<string> path, IDictionary<object, IList<object?>> previousComparisons,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            foreach (var field in fields)
            {
                var actualValue = field.GetValue(actual);
                var expectedValue = field.GetValue(expected);

                var newPath = path.Concat(new[] { field.Name });
                CompareObjects(actualValue, expectedValue, newPath.ToList(), previousComparisons, customMessage, shouldlyMethod);
            }
        }

        private static void CompareProperties(object actual, object expected, IEnumerable<PropertyInfo> properties,
            IList<string> path, IDictionary<object, IList<object?>> previousComparisons,
            string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
        {
            foreach (var property in properties)
            {
                if (property.GetIndexParameters().Length != 0)
                {
                    // There's no sensible way to compare indexers, as there does not exist a way to obtain a collection
                    // of all values in a way that's common to all indexer implementations.
                    throw new NotSupportedException("Comparing types that have indexers is not supported.");
                }

                var actualValue = property.GetValue(actual, Array.Empty<object>());
                var expectedValue = property.GetValue(expected, Array.Empty<object>());

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

        private static void Record(this IDictionary<object, IList<object?>> comparisons, object actual, object? expected)
        {
            if (comparisons.TryGetValue(actual, out IList<object?>? list))
                list.Add(expected);
            else
                comparisons.Add(actual, new List<object?>(new[] { expected }));
        }

        private static bool FindMismatchedElement(
            IEnumerable expected,
            IEnumerable actual,
            out int expectedCount,
            out int actualCount,
            out object? mismatchedElement)
        {
            Dictionary<object, int> elementCounts1 = GetElementCounts(expected, out var nullCount1);
            Dictionary<object, int> elementCounts2 = GetElementCounts(actual, out var nullCount2);

            if (nullCount2 != nullCount1)
            {
                expectedCount = nullCount1;
                actualCount = nullCount2;
                mismatchedElement = null;
                return true;
            }

            foreach (object key in elementCounts1.Keys)
            {
                elementCounts1.TryGetValue(key, out expectedCount);
                elementCounts2.TryGetValue(key, out actualCount);
                if (expectedCount == actualCount)
                {
                    continue;
                }

                mismatchedElement = key;
                return true;
            }

            expectedCount = 0;
            actualCount = 0;
            mismatchedElement = null;
            return false;
        }

        private static Dictionary<object, int> GetElementCounts(IEnumerable collection, out int nullCount)
        {
            var dictionary = new Dictionary<object, int>();
            nullCount = 0;
            foreach (object key in collection)
            {
                if (key == null)
                {
                    ++nullCount;
                }
                else
                {
                    dictionary.TryGetValue(key, out var num);
                    ++num;
                    dictionary[key] = num;
                }
            }

            return dictionary;
        }
    }
}