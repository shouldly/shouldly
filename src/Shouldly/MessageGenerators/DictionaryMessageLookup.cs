namespace Shouldly.MessageGenerators;

/// <summary>
/// Looks up a key in the <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey,TValue}"/> a dictionary assertion was given.
/// </summary>
static class DictionaryMessageLookup
{
    public static bool TryGetValue(object? actual, object key, out object? value)
    {
        if (actual is IDictionary dictionary)
        {
            if (dictionary.Contains(key))
            {
                value = dictionary[key];
                return true;
            }

            value = null;
            return false;
        }

        if (actual is IEnumerable sequence)
        {
            foreach (var entry in sequence)
            {
                if (entry != null && TryReadPair(entry, out var entryKey, out var entryValue) && Equals(entryKey, key))
                {
                    value = entryValue;
                    return true;
                }
            }
        }

        value = null;
        return false;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2075",
        Justification = "KeyValuePair<TKey, TValue> is a BCL type whose public Key and Value properties are preserved by the trimmer.")]
    private static bool TryReadPair(object entry, out object? key, out object? value)
    {
        var type = entry.GetType();
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
        {
            var keyProperty = type.GetProperty(nameof(KeyValuePair<int, int>.Key));
            var valueProperty = type.GetProperty(nameof(KeyValuePair<int, int>.Value));
            if (keyProperty != null && valueProperty != null)
            {
                key = keyProperty.GetValue(entry);
                value = valueProperty.GetValue(entry);
                return true;
            }
        }

        key = null;
        value = null;
        return false;
    }
}
