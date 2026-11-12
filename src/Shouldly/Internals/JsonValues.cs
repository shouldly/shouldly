#if NET8_0_OR_GREATER
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

    private static bool SameJson(object expected, object actual)
    {
#if NET9_0_OR_GREATER
        // Two elements are compared as they are. Unlike a node, an element may repeat a property name.
        if (expected is JsonElement expectedElement && actual is JsonElement actualElement)
            return JsonElement.DeepEquals(expectedElement, actualElement);
#endif

        return JsonNode.DeepEquals(ToNode(expected), ToNode(actual));
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
