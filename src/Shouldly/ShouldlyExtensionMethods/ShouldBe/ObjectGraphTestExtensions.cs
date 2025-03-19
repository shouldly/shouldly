using System.Collections.Immutable;
using System.ComponentModel;

namespace Shouldly;

[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ObjectGraphTestExtensions
{
    private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.Instance;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeEquivalentTo(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        string? customMessage = null) =>
        CompareObjects(actual, expected, new List<string>(), new Dictionary<object, IList<object?>>(), customMessage);

    private static void CompareObjects(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        IList<string> path,
        IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        if (BothValuesAreNull(actual, expected, path, customMessage, shouldlyMethod))
            return;

        var type = GetTypeToCompare(actual, expected, path, customMessage, shouldlyMethod);

        if (type == typeof(string))
        {
            CompareStrings((string)actual, (string)expected, path, customMessage, shouldlyMethod);
        }
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            CompareDictionaries((IDictionary)actual, (IDictionary)expected, path, previousComparisons, customMessage, shouldlyMethod);
        }
        else if (type.IsIReadOnlyDictionary(out var keyType, out var valueType))
        {
            CompareIReadOnlyDictionary(keyType, valueType, actual, expected, path, previousComparisons, customMessage, shouldlyMethod);
        }
        else if (type.IsISet(out var setType))
        {
            CompareISets(setType, actual, expected, path, customMessage, shouldlyMethod);
        }
        else if (type.IsIImmutableSet(out setType))
        {
            CompareIImmutableSets(setType, actual, expected, path, customMessage, shouldlyMethod);
        }
        else if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            CompareEnumerables((IEnumerable)actual, (IEnumerable)expected, path, previousComparisons, customMessage, shouldlyMethod);
        }
        else if (type.IsValueType)
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
            path[^1] += typeName;

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
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            CompareDictionaries((IDictionary)actual, (IDictionary)expected, path, previousComparisons, customMessage, shouldlyMethod);
        }
        else if (type.IsIReadOnlyDictionary(out var keyType, out var valueType))
        {
            CompareIReadOnlyDictionary(keyType, valueType, actual, expected, path, previousComparisons, customMessage, shouldlyMethod);
        }
        else if (type.IsISet(out var setType))
        {
            CompareISets(setType, actual, expected, path, customMessage, shouldlyMethod);
        }
        else if (type.IsIImmutableSet(out setType))
        {
            CompareIImmutableSets(setType, actual, expected, path, customMessage, shouldlyMethod);
        }
        else if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            CompareEnumerables((IEnumerable)actual, (IEnumerable)expected, path, previousComparisons, customMessage, shouldlyMethod);
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

    private static void CompareDictionaries(IDictionary actual, IDictionary expected,
        IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        var keysPath = path.Concat(["Keys"]);
        var actualKeys = new HashSet<object?>(actual.Keys.Cast<object?>());
        var expectedKeys = new HashSet<object?>(expected.Keys.Cast<object?>());
        CompareTypedISets(actualKeys, expectedKeys, keysPath, customMessage, shouldlyMethod);

        foreach (var key in actual.Keys)
        {
            keysPath = path.Concat([
                $"Value [{key.ToStringAwesomely() ?? "<Unknown>"}]"
            ]);
            CompareObjects(actual[key], expected[key], keysPath.ToList(), previousComparisons, customMessage, shouldlyMethod);
        }
    }

    private static void CompareIReadOnlyDictionary(Type keyType, Type valueType, object? actual, object? expected,
        IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            typeof(ObjectGraphTestExtensions)
                .GetMethod(nameof(CompareTypedIReadOnlyDictionaries), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(keyType, valueType)
                .Invoke(null, [actual, expected, path, previousComparisons, customMessage, shouldlyMethod]);
        }
        catch (TargetInvocationException e)
        {
            if (e.InnerException is not ShouldAssertException shouldAssertException)
            {
                throw;
            }
            throw shouldAssertException;
        }
    }

    private static void CompareTypedIReadOnlyDictionaries<TKey, TValue>(
        IReadOnlyDictionary<TKey, TValue> actual, IReadOnlyDictionary<TKey, TValue> expected,
        IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        var keysPath = path.Concat(["Keys"]);
        var actualKeys = new HashSet<TKey>(actual.Keys);
        var expectedKeys = new HashSet<TKey>(expected.Keys);
        CompareTypedISets(actualKeys, expectedKeys, keysPath, customMessage, shouldlyMethod);

        foreach (var key in actual.Keys)
        {
            keysPath = path.Concat([
                $"Value [{key.ToStringAwesomely() ?? "<Unknown>"}]"
            ]);
            CompareObjects(actual[key], expected[key], keysPath.ToList(), previousComparisons, customMessage, shouldlyMethod);
        }
    }

    private static void CompareIImmutableSets(Type setType, object? actual, object? expected,
        IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!) =>
        CompareSets(nameof(CompareTypedIImmutableSets), setType, actual, expected, path, customMessage, shouldlyMethod);

    private static void CompareISets(Type setType, object? actual, object? expected,
        IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!) =>
        CompareSets(nameof(CompareTypedISets), setType, actual, expected, path, customMessage, shouldlyMethod);

    private static void CompareSets(string methodName, Type setType, object? actual, object? expected,
        IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            typeof(ObjectGraphTestExtensions)
                .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(setType)
                .Invoke(null, [actual, expected, path, customMessage, shouldlyMethod]);
        }
        catch (TargetInvocationException e)
        {
            if (e.InnerException is not ShouldAssertException shouldAssertException)
            {
                throw;
            }
            throw shouldAssertException;
        }
    }

    private static void CompareTypedIImmutableSets<T>(IImmutableSet<T> actual, IImmutableSet<T> expected,
        IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        if (actual.SetEquals(expected))
            return;

        var missingInActual = expected.Except(actual).ToList();
        var missingInExpected = actual.Except(expected).ToList();
        CompareTypedSets(missingInActual, missingInExpected, actual, expected, path, customMessage, shouldlyMethod);
    }

    private static void CompareTypedISets<T>(ISet<T> actual, ISet<T> expected,
        IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        if (actual.SetEquals(expected))
            return;

        var missingInActual = expected.Except(actual).ToList();
        var missingInExpected = actual.Except(expected).ToList();
        CompareTypedSets(missingInActual, missingInExpected, actual, expected, path, customMessage, shouldlyMethod);
    }

    private static void CompareTypedSets<T>(List<T> missingInActual, List<T> missingInExpected,
        object? actual, object? expected, IEnumerable<string> path,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        List<string> messages = customMessage is null || customMessage.Length == 0
            ? []
            : [customMessage];

        if (missingInActual.Count > 0)
            messages.Add($"{missingInActual.ToStringAwesomely()} is expected but not found");

        if (missingInExpected.Count > 0)
            messages.Add($"{missingInExpected.ToStringAwesomely()} is not expected but found");

        ThrowException(
            actual,
            expected,
            path,
            messages.Count > 0 ? string.Join("; ", messages) : null,
            shouldlyMethod);
    }

    private static void CompareEnumerables(IEnumerable actual, IEnumerable expected,
        IEnumerable<string> path, IDictionary<object, IList<object?>> previousComparisons,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        var expectedList = expected.Cast<object?>().ToList();
        var actualList = actual.Cast<object?>().ToList();

        if (actualList.Count != expectedList.Count)
        {
            var newPath = path.Concat(["Count"]);
            ThrowException(actualList.Count, expectedList.Count, newPath, customMessage, shouldlyMethod);
        }

        for (var i = 0; i < actualList.Count; i++)
        {
            var newPath = path.Concat([$"Element [{i}]"]);
            CompareObjects(actualList[i], expectedList[i], newPath.ToList(), previousComparisons, customMessage, shouldlyMethod);
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

            var newPath = path.Concat([field.Name]);
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

            var actualValue = property.GetValue(actual, []);
            var expectedValue = property.GetValue(expected, []);

            var newPath = path.Concat([property.Name]);
            CompareObjects(actualValue, expectedValue, newPath.ToList(), previousComparisons, customMessage, shouldlyMethod);
        }
    }

    [DoesNotReturn]
    private static void ThrowException(object? actual, object? expected, IEnumerable<string> path,
        string? customMessage, [CallerMemberName] string shouldlyMethod = null!) =>
        throw new ShouldAssertException(
            new ExpectedEquivalenceShouldlyMessage(expected, actual, path, customMessage, shouldlyMethod).ToString());

    private static bool IsIReadOnlyDictionary(this Type type,
        [NotNullWhen(true)] out Type? keyType, [NotNullWhen(true)] out Type? valueType) =>
        ImplementsDoubleGenericInterface(type, typeof(IReadOnlyDictionary<,>), out keyType, out valueType);

    private static bool IsISet(this Type type, [NotNullWhen(true)] out Type? setType) =>
        ImplementsSingleGenericInterface(type, typeof(ISet<>), out setType);

    private static bool IsIImmutableSet(this Type type, [NotNullWhen(true)] out Type? setType) =>
        ImplementsSingleGenericInterface(type, typeof(IImmutableSet<>), out setType);

    private static bool ImplementsDoubleGenericInterface(this Type type, Type interfaceType,
        [NotNullWhen(true)] out Type? genericType0, [NotNullWhen(true)] out Type? genericType1)
    {
        if (type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
            is { } implementedInterface)
        {
            genericType0 = implementedInterface.GetGenericArguments()[0];
            genericType1 = implementedInterface.GetGenericArguments()[1];
            return true;
        }

        genericType0 = null;
        genericType1 = null;
        return false;
    }

    private static bool ImplementsSingleGenericInterface(this Type type, Type interfaceType, [NotNullWhen(true)] out Type? genericType)
    {
        if (type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
            is { } implementedInterface)
        {
            genericType = implementedInterface.GetGenericArguments()[0];
            return true;
        }

        genericType = null;
        return false;
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