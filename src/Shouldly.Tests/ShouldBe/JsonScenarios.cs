using System.Text.Json;
using System.Text.Json.Nodes;

namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// System.Text.Json's values define no equality, and compared as the collections they look like they are equal by
/// shape alone, or unequal by reference. Shouldly compares two of them as JSON, with nothing registered.
/// </summary>
public class JsonScenarios
{
    private static JsonNode Node(string json) => JsonNode.Parse(json)!;

    private static JsonElement Element(string json) => JsonDocument.Parse(json).RootElement.Clone();

    [Fact]
    public void NodesWithTheSameJsonAreEqual()
    {
        Node("""{"name":"Alice","roles":["admin"]}""").ShouldBe(Node("""{ "roles": ["admin"], "name": "Alice" }"""));
        Node("[1,2,3]").ShouldBe(Node("[1, 2, 3]"));
        Node("1").ShouldBe(JsonValue.Create(1));
        new JsonObject { ["a"] = 1 }.ShouldBe(Node("""{"a":1}"""));
    }

    [Fact]
    public void NodesWithDifferentJsonAreNotEqual()
    {
        Node("""{"name":"Alice"}""").ShouldNotBe(Node("""{"name":"Bob"}"""));
        Node("""{"a":1}""").ShouldNotBe(Node("""{"b":1}"""));
        Node("[1,2,3]").ShouldNotBe(Node("[3,2,1]"));
        Node("\"1\"").ShouldNotBe(Node("1"));

        Should.Throw<ShouldAssertException>(() => Node("""{"name":"Alice"}""").ShouldBe(Node("""{"name":"Bob"}""")));
        Should.Throw<ShouldAssertException>(() => Node("[1,2,3]").ShouldNotBe(Node("[1, 2, 3]")));
    }

    // Both have no items, and they aren't the same JSON.
    [Fact]
    public void AnEmptyArrayIsNotAnEmptyObject()
    {
        Node("""{"a":[]}""").ShouldNotBe(Node("""{"a":{}}"""));
        Element("""{"a":[]}""").ShouldNotBe(Element("""{"a":{}}"""));
    }

    [Fact]
    public void ElementsAreComparedAsJson()
    {
        Element("""{"name":"Alice","roles":["admin"]}""").ShouldBe(Element("""{ "roles": ["admin"], "name": "Alice" }"""));
        Element("""{"name":"Alice"}""").ShouldNotBe(Element("""{"name":"Bob"}"""));
        Element("null").ShouldBe(Element("null"));
        Element("null").ShouldNotBe(Element("0"));

        default(JsonElement).ShouldBe(default);
        default(JsonElement).ShouldNotBe(Element("null"));
    }

    [Fact]
    public void ANodeAndAnElementAreComparedAsJson()
    {
        ((object)Node("""{"a":1}""")).ShouldBe(Element("""{"a":1}"""));
        ((object)Element("""{"a":1}""")).ShouldNotBe(Node("""{"a":2}"""));
    }

    [Fact]
    public void JsonIsComparedAsJsonWhereverItSits()
    {
        new List<JsonNode> { Node("""{"a":1}"""), Node("[1]") }.ShouldBe([Node("""{ "a": 1 }"""), Node("[ 1 ]")]);
        new Dictionary<string, JsonElement> { ["x"] = Element("""{"a":1}""") }
            .ShouldBe(new Dictionary<string, JsonElement> { ["x"] = Element("""{ "a": 1 }""") });
        (1, Node("""{"a":1}""")).ShouldBe((1, Node("""{ "a": 1 }""")));

        new List<JsonNode> { Node("""{"a":1}""") }.ShouldContain(Node("""{ "a": 1 }"""));
        new List<JsonNode> { Node("""{"a":1}""") }.ShouldNotContain(Node("""{ "a": 2 }"""));
        Should.Throw<ShouldAssertException>(() =>
            new List<JsonNode> { Node("""{"a":1}""") }.ShouldBe([Node("""{"a":2}""")]));
    }

    [Fact]
    public void NodesShouldFail() =>
        Verify.ShouldFail(() =>
            Node("""{"name":"Alice","roles":["admin"]}""").ShouldBe(Node("""{"name":"Bob","roles":["guest"]}"""), "Some additional context"));

    [Fact]
    public void ElementsShouldFail() =>
        Verify.ShouldFail(() =>
            Element("""{"name":"Alice"}""").ShouldBe(Element("""{"name":"Bob"}"""), "Some additional context"));

    [Fact]
    public void ListOfNodesShouldFail() =>
        Verify.ShouldFail(() =>
            new List<JsonNode> { Node("""{"a":1}"""), Node("""{"b":2}""") }
                .ShouldBe([Node("""{"a":1}"""), Node("""{"b":3}""")], "Some additional context"));
}
