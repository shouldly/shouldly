namespace Shouldly;

static class TypeExtensions
{
    public static bool TryGetEnumerable(this object obj, [NotNullWhen(true)] out IEnumerable? enumerable)
    {
        enumerable = obj as IEnumerable;

        if (enumerable == null && obj != null && TryGetMemoryArray(obj, out var array))
        {
            enumerable = array;
        }

        return enumerable != null;
    }

    // Memory<T>/ReadOnlyMemory<T> don't implement IEnumerable, so the boxed value
    // can't be enumerated directly. We invoke their public ToArray() method via
    // reflection on the already-closed generic type — no MakeGenericMethod is
    // needed, so this path is safe under native AOT. ToArray allocates once per
    // call, which is acceptable for the failure-message formatting path; the
    // equality path uses Memory<T>-typed ShouldBe overloads that walk the Span
    // directly without allocating.
    [UnconditionalSuppressMessage("Trimming", "IL2075",
        Justification = "Targets Memory<T>/ReadOnlyMemory<T> — BCL types whose public ToArray() method is part of their stable contract and preserved by the trimmer.")]
    private static bool TryGetMemoryArray(object obj, [NotNullWhen(true)] out Array? array)
    {
        var type = obj.GetType();
        if (type.IsGenericType)
        {
            var def = type.GetGenericTypeDefinition();
            if (def == typeof(Memory<>) || def == typeof(ReadOnlyMemory<>))
            {
                var toArray = type.GetMethod("ToArray", BindingFlags.Public | BindingFlags.Instance, binder: null, types: Type.EmptyTypes, modifiers: null);
                if (toArray is not null)
                {
                    array = (Array)toArray.Invoke(obj, null)!;
                    return true;
                }
            }
        }

        array = null;
        return false;
    }
}
