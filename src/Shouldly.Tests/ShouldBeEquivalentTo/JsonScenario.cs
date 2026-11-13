using System.Text.Json;
using System.Text.Json.Nodes;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

// A System.Text.Json value is compared as JSON, whole, wherever it sits: its public members say little of it.
public class JsonScenario
{
    private static JsonNode Node(string json) => JsonNode.Parse(json)!;

    private static JsonElement Element(string json) => JsonDocument.Parse(json).RootElement.Clone();

    [Fact]
    public void ShouldPassWhenNodesHoldTheSameJson()
    {
        Node("""{"name":"Alice","roles":["admin"]}""").ShouldBeEquivalentTo(Node("""{ "roles": ["admin"], "name": "Alice" }"""));
        Node("[1,2]").ShouldBeEquivalentTo(Node("[1, 2]"));
    }

    [Fact]
    public void ShouldFailWhenNodesDiffer()
    {
        var subject = Node("""{"name":"Alice","roles":["admin"]}""");
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(Node("""{"name":"Bob","roles":["guest"]}"""), "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenAnEmptyArrayIsComparedWithAnEmptyObject() =>
        Should.Throw<ShouldAssertException>(() =>
            Node("""{"a":[]}""").ShouldBeEquivalentTo(Node("""{"a":{}}""")));

    [Fact]
    public void ShouldCompareElementsAsJson()
    {
        Element("""{"name":"Alice"}""").ShouldBeEquivalentTo(Element("""{ "name": "Alice" }"""));
        Should.Throw<ShouldAssertException>(() =>
            Element("""{"name":"Alice"}""").ShouldBeEquivalentTo(Element("""{"name":"Bob"}""")));
    }

    [Fact]
    public void ShouldFailWhenAJsonMemberDiffers()
    {
        var subject = new Message { Id = 1, Body = Element("""{"id":1}"""), Extra = Node("""{"tags":["a"]}""") };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(
                new Message { Id = 1, Body = Element("""{"id":2}"""), Extra = Node("""{"tags":["b"]}""") },
                "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenJsonMembersHoldTheSameJson()
    {
        var subject = new Message { Id = 1, Body = Element("""{"id":1}"""), Extra = Node("""{"tags":["a"]}""") };

        subject.ShouldBeEquivalentTo(new Message { Id = 1, Body = Element("""{ "id": 1 }"""), Extra = Node("""{ "tags": ["a"] }""") });
        subject.ShouldBeEquivalentTo(new { Body = Element("""{ "id": 1 }""") });
    }

    // An element is valid JSON to whatever read it, which may have allowed more than a parser does by default.
    [Fact]
    public void ShouldCompareElementsReadWithOptionsAParserDoesNotDefaultTo()
    {
        var nested = new string('[', 65) + new string(']', 65);
        JsonElement Nested(string json) => JsonDocument.Parse(json, new JsonDocumentOptions { MaxDepth = 200 }).RootElement.Clone();

        Nested(nested).ShouldBeEquivalentTo(Nested(nested));
        new { Body = Nested(nested) }.ShouldBeEquivalentTo(new { Body = Nested(nested) });
        Should.Throw<ShouldAssertException>(() => Nested(nested).ShouldBeEquivalentTo(Nested($"[{nested}]")));
    }

    // An element may repeat a property name, which a node can't hold.
    [Fact]
    public void ShouldCompareElementsThatRepeatAPropertyName()
    {
        Element("""{"x":1,"x":2}""").ShouldBeEquivalentTo(Element("""{ "x": 1, "x": 2 }"""));
        new { Body = Element("""{"x":1,"x":2}""") }.ShouldBeEquivalentTo(new { Body = Element("""{"x":1,"x":2}""") });
        Should.Throw<ShouldAssertException>(() =>
            Element("""{"x":1,"x":2}""").ShouldBeEquivalentTo(Element("""{"x":1,"x":3}""")));
    }

    // A JsonArray is JSON, compared whole, and not a list of nodes.
    [Fact]
    public void ShouldFailWhenAJsonArrayIsComparedWithAList()
    {
        Should.Throw<ShouldAssertException>(() => new JsonArray().ShouldBeEquivalentTo(new List<JsonNode>()));
        Should.Throw<ShouldAssertException>(() => new List<JsonNode>().ShouldBeEquivalentTo(new JsonArray()));
    }

    // A JsonDocument is compared by its one member, the root element.
    [Fact]
    public void ShouldCompareDocumentsByTheirRootElement()
    {
        using var subject = JsonDocument.Parse("""{"a":1}""");
        using var same = JsonDocument.Parse("""{ "a": 1 }""");
        using var different = JsonDocument.Parse("""{"a":2}""");

        subject.ShouldBeEquivalentTo(same);
        Should.Throw<ShouldAssertException>(() => subject.ShouldBeEquivalentTo(different));
    }

    private class Message
    {
        public int Id { get; set; }
        public JsonElement Body { get; set; }
        public JsonNode? Extra { get; set; }
    }
}
