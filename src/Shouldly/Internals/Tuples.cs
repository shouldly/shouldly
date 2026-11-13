using System.Collections.Concurrent;

namespace Shouldly.Internals;

/// <summary>
/// Compares two boxed tuples of any type arguments item by item: a <see cref="KeyValuePair{TKey, TValue}"/> as its key
/// and its value, and a <see cref="ValueTuple{T1, T2}"/> or a <see cref="Tuple{T1, T2}"/> (of any arity) as its items.
/// A tuple's own <c>Equals</c> compares a collection-typed item by reference, so without this a dictionary's entries
/// (issue #1408), or a tuple holding a list (issue #1429), would compare differently from the items on their own.
/// </summary>
/// <remarks>
/// Only these BCL types are taken apart. A record, an anonymous type or a struct keeps its own <c>Equals</c>, which is
/// its author's to define, and <see cref="Failures.ExplanationWording"/> points at <c>ShouldBeEquivalentTo</c> when that compares a
/// collection by reference.
/// </remarks>
static class Tuples
{
    // A ValueTuple or Tuple holds at most seven items of its own. An eighth type argument is Rest, a tuple holding the
    // items from the eighth on.
    private const int ItemsBeforeRest = 7;
    private const string Rest = "Rest";

    private static readonly ConcurrentDictionary<Type, Accessors?> AccessorsByType = new();

    /// <summary>
    /// Whether values of <paramref name="type"/> are compared item by item by <see cref="TryEquals"/>.
    /// </summary>
    public static bool ComparesItemByItem(Type type) => FindDefinition(type) is not null;

    /// <summary>
    /// Whether <paramref name="expected"/> and <paramref name="actual"/> are tuples of one kind and arity, which
    /// <see cref="TryEquals"/> compares item by item.
    /// </summary>
    public static bool AreComparedItemByItem(object expected, object actual) => FindBoth(expected, actual) is not null;

    /// <summary>
    /// When <paramref name="expected"/> and <paramref name="actual"/> are both tuples of one kind and arity (both
    /// <see cref="KeyValuePair{TKey, TValue}"/>s, both two-item <see cref="ValueTuple{T1, T2}"/>s, and so on),
    /// sets <paramref name="result"/> to whether each of their items is equal and returns <c>true</c>.
    /// Each item is compared with <paramref name="elementComparer"/>, as an element of a sequence held as objects is,
    /// so what decides is what the item's runtime type defines. <c>Equals</c> is called on the expected item.
    /// </summary>
    public static bool TryEquals(object expected, object actual, IEqualityComparer<object?> elementComparer, out bool result)
    {
        if (FindBoth(expected, actual) is not (var expectedAccessors, var actualAccessors))
        {
            result = false;
            return false;
        }

        for (var i = 0; i < expectedAccessors.Items.Length; i++)
        {
            if (!ItemEquals(expectedAccessors.Items[i], actualAccessors.Items[i], expected, actual, elementComparer))
            {
                result = false;
                return true;
            }
        }

        result = true;
        return true;
    }

    /// <summary>
    /// When <paramref name="expected"/> and <paramref name="actual"/> are tuples <see cref="TryEquals"/> compares item by
    /// item, names the first item it found unequal (<c>Key</c>, <c>Value</c>, or <c>Item1</c>, <c>Item2</c> and so on,
    /// numbered as C# numbers them, so the first item held in <c>Rest</c> is <c>Item8</c>) and gives the two items, for
    /// a failure message that says where two tuples differ.
    /// </summary>
    public static bool TryFindUnequalComponent(object expected, object actual, IEqualityComparer<object?> elementComparer,
        [NotNullWhen(true)] out string? name, out object? expectedComponent, out object? actualComponent)
    {
        name = null;
        expectedComponent = actualComponent = null;
        if (FindBoth(expected, actual) is not (var expectedAccessors, var actualAccessors))
            return false;

        for (var i = 0; i < expectedAccessors.Items.Length; i++)
        {
            var expectedItem = expectedAccessors.Items[i];
            var actualItem = actualAccessors.Items[i];
            if (ItemEquals(expectedItem, actualItem, expected, actual, elementComparer))
                continue;

            name = expectedItem.Name;
            expectedComponent = expectedItem.GetValue(expected);
            actualComponent = actualItem.GetValue(actual);
            return true;
        }

        return false;
    }

    /// <summary>
    /// When <paramref name="pair"/> is a <see cref="KeyValuePair{TKey, TValue}"/>, gives its key and its value.
    /// </summary>
    public static bool TryDeconstructKeyValuePair(object pair, out object? key, out object? value)
    {
        if (FindKeyValuePair(pair) is not { } accessors)
        {
            key = value = null;
            return false;
        }

        key = accessors.Items[0].GetValue(pair);
        value = accessors.Items[1].GetValue(pair);
        return true;
    }

    /// <summary>
    /// Whether two <see cref="KeyValuePair{TKey, TValue}"/>s have equal keys, compared as <see cref="TryEquals"/>
    /// compares them. False when either isn't a pair.
    /// </summary>
    public static bool KeysEqual(object expected, object actual, IEqualityComparer<object?> elementComparer) =>
        FindKeyValuePair(expected) is { } expectedAccessors &&
        FindKeyValuePair(actual) is { } actualAccessors &&
        ItemEquals(expectedAccessors.Items[0], actualAccessors.Items[0], expected, actual, elementComparer);

    private static Accessors? FindKeyValuePair(object value) =>
        Find(value) is { } accessors && accessors.Definition == typeof(KeyValuePair<,>) ? accessors : null;

    // Each item is compared as a value on its own is: by the element comparer, which consults equality rules first
    // and asks what the item's runtime type defines, called on the expected item.
    private static bool ItemEquals(Item expectedItem, Item actualItem, object expected, object actual, IEqualityComparer<object?> elementComparer) =>
        elementComparer.Equals(expectedItem.GetValue(expected), actualItem.GetValue(actual));

    // Both tuples, and of one kind and arity, so their items line up one for one. The definition alone doesn't give
    // the arity: every tuple of eight items or more has the same one, and the rest of its items in Rest.
    private static (Accessors Expected, Accessors Actual)? FindBoth(object expected, object actual) =>
        Find(expected) is { } expectedAccessors &&
        Find(actual) is { } actualAccessors &&
        expectedAccessors.Definition == actualAccessors.Definition &&
        expectedAccessors.Items.Length == actualAccessors.Items.Length
            ? (expectedAccessors, actualAccessors)
            : null;

    private static Accessors? Find(object value) =>
        value.GetType() is { IsGenericType: true } type
            ? AccessorsByType.GetOrAdd(type, static type => FindAccessors(type))
            : null;

    private static Type? FindDefinition(Type type)
    {
        if (!type.IsGenericType)
            return null;

        var definition = type.GetGenericTypeDefinition();
        return definition == typeof(KeyValuePair<,>) || IsValueTuple(definition) || IsTuple(definition) ? definition : null;
    }

    private static bool IsValueTuple(Type definition) =>
        definition == typeof(ValueTuple<>) || definition == typeof(ValueTuple<,>) || definition == typeof(ValueTuple<,,>) ||
        definition == typeof(ValueTuple<,,,>) || definition == typeof(ValueTuple<,,,,>) || definition == typeof(ValueTuple<,,,,,>) ||
        definition == typeof(ValueTuple<,,,,,,>) || definition == typeof(ValueTuple<,,,,,,,>);

    private static bool IsTuple(Type definition) =>
        definition == typeof(Tuple<>) || definition == typeof(Tuple<,>) || definition == typeof(Tuple<,,>) ||
        definition == typeof(Tuple<,,,>) || definition == typeof(Tuple<,,,,>) || definition == typeof(Tuple<,,,,,>) ||
        definition == typeof(Tuple<,,,,,,>) || definition == typeof(Tuple<,,,,,,,>);

    // The getters are read from the already-constructed tuple type, so no MakeGenericType is needed. Rooting the generic
    // definitions' members keeps them for every instantiation, as StringHelpers' pair formatting needs too.
#if NET5_0_OR_GREATER
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(KeyValuePair<,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<,,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<,,,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<,,,,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicFields, typeof(ValueTuple<,,,,,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<,,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<,,,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<,,,,,,>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Tuple<,,,,,,,>))]
#endif
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "The public properties and fields of KeyValuePair<,>, ValueTuple<...> and Tuple<...> are rooted by the DynamicDependency attributes above.")]
    private static Accessors? FindAccessors(Type type)
    {
        if (FindDefinition(type) is not { } definition)
            return null;

        if (definition == typeof(KeyValuePair<,>))
        {
            return new(definition, [
                Item.Of(type.GetProperty(nameof(KeyValuePair<int, int>.Key))!),
                Item.Of(type.GetProperty(nameof(KeyValuePair<int, int>.Value))!)
            ]);
        }

        // Item1 to Item7 are read from the tuple, and the items after them from its Rest, numbered on from Item8 as
        // C# numbers them, and from that tuple's Rest in turn.
        var isValueTuple = IsValueTuple(definition);
        var items = new List<Item>();
        Func<object, object?>? readHolder = null;
        var restName = Rest;
        for (var holderType = type;;)
        {
            var typeArguments = holderType.GenericTypeArguments;
            for (var i = 0; i < Math.Min(typeArguments.Length, ItemsBeforeRest); i++)
                items.Add(Member(holderType, $"Item{i + 1}", isValueTuple).Within(readHolder, $"Item{items.Count + 1}"));

            if (typeArguments.Length <= ItemsBeforeRest)
                break;

            // A Rest that isn't a tuple of the same kind (possible only in a default ValueTuple, or when it is
            // declared as a base type) is an item like any other.
            var rest = Member(holderType, Rest, isValueTuple).Within(readHolder, restName);
            var restType = typeArguments[ItemsBeforeRest];
            if (FindDefinition(restType) is not { } restDefinition ||
                !(isValueTuple ? IsValueTuple(restDefinition) : IsTuple(restDefinition)))
            {
                items.Add(rest);
                break;
            }

            readHolder = rest.GetValue;
            restName += $".{Rest}";
            holderType = restType;
        }

        return new(definition, [.. items]);
    }

    // A ValueTuple's items are fields and a Tuple's are properties.
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Called only by FindAccessors, whose DynamicDependency attributes root the public fields of ValueTuple<...> and the public properties of Tuple<...>.")]
    private static Item Member(Type tupleType, string name, bool isValueTuple) =>
        isValueTuple ? Item.Of(tupleType.GetField(name)!) : Item.Of(tupleType.GetProperty(name)!);

    private sealed record Accessors(Type Definition, Item[] Items);

    /// <summary>
    /// One item of a tuple: its name, and how to read it.
    /// </summary>
    private sealed record Item(string Name, Func<object, object?> GetValue)
    {
        public static Item Of(PropertyInfo property) => new(property.Name, property.GetValue);

        public static Item Of(FieldInfo field) => new(field.Name, field.GetValue);

        /// <summary>
        /// This item under <paramref name="name"/>, read from the tuple that <paramref name="readHolder"/> finds in
        /// the value given: the <c>Rest</c> of a tuple of eight items or more. Null when it is read from the value itself.
        /// </summary>
        public Item Within(Func<object, object?>? readHolder, string name)
        {
            var getValue = GetValue;
            return new(name, readHolder is null
                ? getValue
                : tuple => readHolder(tuple) is { } holder ? getValue(holder) : null);
        }
    }
}
