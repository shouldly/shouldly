#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Shouldly.Internals;

/// <summary>
/// The JSON values of <c>System.Text.Json</c>: a <see cref="JsonNode"/> or a <see cref="JsonElement"/>. Neither
/// defines equality, and neither can be compared as anything else it looks like. A <see cref="JsonObject"/> is a
/// dictionary of values that compare by reference, an empty <see cref="JsonArray"/> has the same items as an empty
/// <see cref="JsonObject"/>, and a <see cref="JsonElement"/> shows only the kind of value it is among its members.
/// So <see cref="Equality"/> and <c>ShouldBeEquivalentTo</c> compare two of them as JSON, whole, and a failure
/// message shows them as JSON.
/// </summary>
/// <remarks>
/// Unlike an equality rule, this isn't registered: <c>System.Text.Json</c> is part of .NET from .NET 8, where
/// Shouldly can refer to it without reflection. An equality rule registered for one of these types still decides
/// first. On older frameworks Shouldly doesn't know the types, and their users register the rule.
/// </remarks>
static class JsonValues
{
    public static bool IsJsonType(Type type) =>
        type == typeof(JsonElement) || typeof(JsonNode).IsAssignableFrom(type);

    /// <summary>
    /// When both values are JSON, sets <paramref name="result"/> to whether they are the same JSON and returns
    /// <c>true</c>. Property order and insignificant whitespace don't count.
    /// </summary>
    public static bool TryEquals(object expected, object actual, out bool result)
    {
        if (expected is not (JsonNode or JsonElement) || actual is not (JsonNode or JsonElement))
        {
            result = false;
            return false;
        }

        // A default JsonElement holds no JSON, so it equals only another.
        var expectedHasValue = HasValue(expected);
        result = expectedHasValue == HasValue(actual) && (!expectedHasValue || SameJson(expected, actual));
        return true;
    }

    /// <summary>The JSON text of a JSON value, or null when <paramref name="value"/> isn't one.</summary>
    public static string? TextOf(object value) => value switch
    {
        JsonNode node => node.ToJsonString(),
        JsonElement { ValueKind: JsonValueKind.Undefined } => "undefined",
        JsonElement element => element.GetRawText(),
        _ => null
    };

    // Two elements are compared as they are. A node can't hold an object that repeats a property name, which an
    // element may, so turning such an element into a node throws.
    private static bool SameJson(object expected, object actual) =>
        expected is JsonElement expectedElement && actual is JsonElement actualElement
            ? SameElement(expectedElement, actualElement)
            : JsonNode.DeepEquals(ToNode(expected), ToNode(actual));

    // JsonElement.DeepEquals does this from .NET 9. Shouldly also runs on .NET 8, so it walks objects and arrays
    // itself wherever it runs, and what it does is then the same, and tested, on each. A string or a number is still
    // compared by the runtime.
    private static bool SameElement(JsonElement expected, JsonElement actual)
    {
        if (expected.ValueKind != actual.ValueKind)
            return false;

        switch (expected.ValueKind)
        {
            case JsonValueKind.Object:
                RuntimeHelpers.EnsureSufficientExecutionStack();
                return SameProperties(expected, actual);

            case JsonValueKind.Array:
                RuntimeHelpers.EnsureSufficientExecutionStack();
                return SameItems(expected, actual);

            case JsonValueKind.String:
            case JsonValueKind.Number:
                return JsonNode.DeepEquals(JsonValue.Create(expected), JsonValue.Create(actual));

            default:
                // true, false and null are each the only value of their kind.
                return true;
        }
    }

    private static bool SameItems(JsonElement expected, JsonElement actual)
    {
        if (expected.GetArrayLength() != actual.GetArrayLength())
            return false;

        using var expectedItems = expected.EnumerateArray();
        using var actualItems = actual.EnumerateArray();
        while (expectedItems.MoveNext() && actualItems.MoveNext())
        {
            if (!SameElement(expectedItems.Current, actualItems.Current))
                return false;
        }

        return true;
    }

    // Properties are matched by name, in any order. Those that share a name are matched in the order they come in.
    private static bool SameProperties(JsonElement expected, JsonElement actual)
    {
        var actualValues = new Dictionary<string, Queue<JsonElement>>(StringComparer.Ordinal);
        var unmatched = 0;
        foreach (var property in actual.EnumerateObject())
        {
            if (!actualValues.TryGetValue(property.Name, out var values))
                actualValues[property.Name] = values = new();

            values.Enqueue(property.Value);
            unmatched++;
        }

        foreach (var property in expected.EnumerateObject())
        {
            if (!actualValues.TryGetValue(property.Name, out var values) ||
                values.Count == 0 ||
                !SameElement(property.Value, values.Dequeue()))
                return false;

            unmatched--;
        }

        return unmatched == 0;
    }

    private static bool HasValue(object json) =>
        json is not JsonElement { ValueKind: JsonValueKind.Undefined };

    // JSON's null is a null node. An element is wrapped rather than parsed again from its text: it was valid to
    // whatever read it, which may have allowed more than a parser does by default (nesting deeper than 64,
    // comments, trailing commas).
    private static JsonNode? ToNode(object json) =>
        json is not JsonElement element
            ? (JsonNode)json
            : element.ValueKind switch
            {
                JsonValueKind.Null => null,
                JsonValueKind.Object => JsonObject.Create(element),
                JsonValueKind.Array => JsonArray.Create(element),
                _ => JsonValue.Create(element)
            };
}
#endif
