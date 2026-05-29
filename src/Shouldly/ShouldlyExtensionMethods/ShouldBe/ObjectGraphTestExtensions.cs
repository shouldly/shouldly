using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for object graph comparison assertions
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ObjectGraphTestExtensions
{
    private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.Instance;

    /// <summary>
    /// Asserts that an object is equivalent to another object by comparing all properties and fields.
    /// Supports value types, reference types, strings, and enumerables.
    /// </summary>
    [RequiresUnreferencedCode("Walks the actual/expected object graph using reflection over each runtime type's public fields and properties. The trimmer cannot statically determine which members are read.")]
    public static void ShouldBeEquivalentTo(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        CompareObjects(actual, expected, new List<string>(), new Dictionary<object, IList<object?>>(), customMessage, actualExpression: actualExpression);
    }

    [RequiresUnreferencedCode("Reflects over fields and properties of the runtime type.")]
    private static void CompareObjects(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        IList<string> path,
        IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        if (BothValuesAreNull(actual, expected, path, customMessage, shouldlyMethod, actualExpression))
            return;

        var type = GetTypeToCompare(actual, expected, path, customMessage, shouldlyMethod, actualExpression);

        // Under an active AssertionScope a recorded failure does not throw, so GetTypeToCompare
        // returns null on a type mismatch. Stop here rather than casting to a mismatched type.
        if (type == null)
            return;

        if (type == typeof(string))
        {
            CompareStrings((string)actual, (string)expected, path, customMessage, shouldlyMethod, actualExpression);
        }
        else if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            CompareEnumerables((IEnumerable)actual, (IEnumerable)expected, path, previousComparisons, customMessage, shouldlyMethod, actualExpression);
        }
        else if (type.IsValueType)
        {
            CompareValueTypes((ValueType)actual, (ValueType)expected, path, customMessage, shouldlyMethod, actualExpression);
        }
        else
        {
            CompareReferenceTypes(actual, expected, type, path, previousComparisons, customMessage, shouldlyMethod, actualExpression);
        }
    }

    private static bool BothValuesAreNull(
        [NotNullWhen(false)] object? actual,
        [NotNullWhen(false)] object? expected,
        IEnumerable<string> path,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        if (expected == null)
        {
            if (actual == null)
                return true;

            ThrowException(actual, expected, path, customMessage, shouldlyMethod, actualExpression);
            // A failure was recorded (the call above does not throw inside an AssertionScope).
            // Signal the caller to stop comparing so it does not dereference the null value.
            return true;
        }
        else if (actual == null)
        {
            ThrowException(actual, expected, path, customMessage, shouldlyMethod, actualExpression);
            return true;
        }

        return false;
    }

    private static Type? GetTypeToCompare(object actual, object expected, IList<string> path,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        var expectedType = expected.GetType();
        var actualType = actual.GetType();

        if (actualType != expectedType)
        {
            ThrowException(actualType, expectedType, path, customMessage, shouldlyMethod, actualExpression);
            // The recorded failure does not throw inside an AssertionScope; return null so the
            // caller stops rather than casting the value to the mismatched type.
            return null;
        }

        var typeName = $" [{actualType.FullName}]";
        if (path.Count == 0)
            path.Add(typeName);
        else
            path[path.Count - 1] += typeName;

        return actualType;
    }

    private static void CompareValueTypes(ValueType actual, ValueType expected, IEnumerable<string> path,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        if (!actual.Equals(expected))
            ThrowException(actual, expected, path, customMessage, shouldlyMethod, actualExpression);
    }

    [RequiresUnreferencedCode("Reflects over fields and properties of the runtime type.")]
    private static void CompareReferenceTypes(object actual, object expected, Type type,
        IList<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        if (ReferenceEquals(actual, expected) ||
            previousComparisons.Contains(actual, expected))
            return;

        previousComparisons.Record(actual, expected);

        if (type == typeof(string))
        {
            CompareStrings((string)actual, (string)expected, path, customMessage, shouldlyMethod, actualExpression);
        }
        else if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            CompareEnumerables((IEnumerable)actual, (IEnumerable)expected, path, previousComparisons, customMessage, shouldlyMethod, actualExpression);
        }
        else
        {
            var fields = type.GetFields(DefaultBindingFlags);
            CompareFields(actual, expected, fields, path, previousComparisons, customMessage, shouldlyMethod, actualExpression);

            var properties = type.GetProperties(DefaultBindingFlags);
            CompareProperties(actual, expected, properties, path, previousComparisons, customMessage, shouldlyMethod, actualExpression);
        }
    }

    private static void CompareStrings(string actual, string expected, IEnumerable<string> path,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        if (!actual.Equals(expected, StringComparison.Ordinal))
            ThrowException(actual, expected, path, customMessage, shouldlyMethod, actualExpression);
    }

    [RequiresUnreferencedCode("Recurses into CompareObjects which reflects over the runtime type.")]
    private static void CompareEnumerables(IEnumerable actual, IEnumerable expected,
        IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        var expectedList = expected.Cast<object?>().ToList();
        var actualList = actual.Cast<object?>().ToList();

        if (actualList.Count != expectedList.Count)
        {
            var newPath = path.Concat(["Count"]);
            ThrowException(actualList.Count, expectedList.Count, newPath, customMessage, shouldlyMethod, actualExpression);
            // Counts differ and the failure has been recorded; stop before indexing the lists,
            // which would throw inside an AssertionScope where the call above does not.
            return;
        }

        for (var i = 0; i < actualList.Count; i++)
        {
            var newPath = path.Concat([$"Element [{i}]"]);
            CompareObjects(actualList[i], expectedList[i], newPath.ToList(), previousComparisons, customMessage, shouldlyMethod, actualExpression);
        }
    }

    [RequiresUnreferencedCode("Recurses into CompareObjects which reflects over the runtime type.")]
    private static void CompareFields(object actual, object expected, IEnumerable<FieldInfo> fields,
        IList<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        foreach (var field in fields)
        {
            var actualValue = field.GetValue(actual);
            var expectedValue = field.GetValue(expected);

            var newPath = path.Concat([field.Name]);
            CompareObjects(actualValue, expectedValue, newPath.ToList(), previousComparisons, customMessage, shouldlyMethod, actualExpression);
        }
    }

    [RequiresUnreferencedCode("Recurses into CompareObjects which reflects over the runtime type.")]
    private static void CompareProperties(object actual, object expected, IEnumerable<PropertyInfo> properties,
        IList<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        foreach (var property in properties)
        {
            if (property.GetIndexParameters().Length != 0)
            {
                // There's no sensible way to compare indexers, as there does not exist a way to obtain a collection
                // of all values in a way that's common to all indexer implementations.
                throw new NotSupportedException("Comparing types that have indexers is not supported.");
            }

            var actualValue = property.GetValue(actual, []);
            var expectedValue = property.GetValue(expected, []);

            var newPath = path.Concat([property.Name]);
            CompareObjects(actualValue, expectedValue, newPath.ToList(), previousComparisons, customMessage, shouldlyMethod, actualExpression);
        }
    }

    private static void ThrowException(object? actual, object? expected, IEnumerable<string> path,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        ThrowHelper.ThrowOrRecord(new ShouldAssertException(
            new ExpectedEquivalenceShouldlyMessage(expected, actual, path, customMessage, shouldlyMethod, actualExpression).ToString()));
    }

    private static bool Contains(this IDictionary<object, IList<object?>> comparisons, object actual, object? expected) =>
        comparisons.TryGetValue(actual, out var list)
        && list.Contains(expected);

    private static void Record(this IDictionary<object, IList<object?>> comparisons, object actual, object? expected)
    {
        if (comparisons.TryGetValue(actual, out var list))
        {
            list.Add(expected);
        }
        else
        {
            comparisons.Add(actual, new List<object?>([expected]));
        }
    }
}
