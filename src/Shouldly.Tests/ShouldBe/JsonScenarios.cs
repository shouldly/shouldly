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

    // An element is valid JSON to whatever read it, which may have allowed more than a parser does by default:
    // deeper nesting than 64, comments, trailing commas. Comparing it mustn't read its text again more strictly.
    [Fact]
    public void ElementsReadWithOptionsAParserDoesNotDefaultToAreCompared()
    {
        var nested = new string('[', 65) + new string(']', 65);
        var options = new JsonDocumentOptions { MaxDepth = 200, CommentHandling = JsonCommentHandling.Skip, AllowTrailingCommas = true };
        JsonElement Read(string json) => JsonDocument.Parse(json, options).RootElement.Clone();

        Read(nested).ShouldBe(Read(nested));
        Read(nested).ShouldNotBe(Read($"[{nested}]"));
        Read("""{/* a comment */"a":[1,2,],}""").ShouldBe(Read("""{"a":[1,2]}"""));
        Read("""{/* a comment */"a":[1,2,],}""").ShouldNotBe(Read("""{"a":[1,3]}"""));

        ((object)Read(nested)).ShouldBe(JsonNode.Parse(nested, documentOptions: options));
        ((object)JsonNode.Parse("""{"a":[1,2]}""")!).ShouldBe(Read("""{/* a comment */"a":[1,2,],}"""));
        Should.Throw<ShouldAssertException>(() => Read(nested).ShouldBe(Read($"[{nested}]")));
    }

    // Shouldly walks two elements itself, so that it compares them the same way on .NET 8, which has no
    // JsonElement.DeepEquals. This holds what it says to what .NET says.
    [Theory]
    [InlineData("""{"x":1,"x":2}""", """{"x":1,"x":2}""")]
    [InlineData("""{"x":1,"x":2}""", """{"x":1,"x":3}""")]
    [InlineData("""{"x":1,"x":2}""", """{"x":2,"x":1}""")]
    [InlineData("""{"x":1,"y":0,"x":2}""", """{"y":0,"x":1,"x":2}""")]
    [InlineData("""{"x":1,"x":2}""", """{"x":1}""")]
    [InlineData("""{"x":1}""", """{"x":1,"x":1}""")]
    [InlineData("""{"x":1}""", """{"x":1,"y":2}""")]
    [InlineData("""{"x":1,"y":2}""", """{"x":1}""")]
    [InlineData("""{"x":1}""", """{"y":1}""")]
    [InlineData("""{"x":1,"y":2}""", """{"y":2,"x":1}""")]
    [InlineData("[1,2]", "[1,2,3]")]
    [InlineData("[1,2,3]", "[1,2]")]
    [InlineData("[1,2]", "[2,1]")]
    [InlineData("[true]", "[true]")]
    [InlineData("[true]", "[false]")]
    [InlineData("[null]", "[false]")]
    [InlineData("[null]", "[null]")]
    [InlineData("""["a"]""", """["\u0061"]""")]
    [InlineData("""["a"]""", """["b"]""")]
    [InlineData("[1.0]", "[1]")]
    [InlineData("[1e2]", "[100]")]
    [InlineData("[1]", "[2]")]
    [InlineData("1", "\"1\"")]
    [InlineData("{}", "[]")]
    [InlineData("{}", "{}")]
    [InlineData("[]", "[]")]
    [InlineData("""{"a":[{"b":null,"c":[true,"s",1.5]}]}""", """{ "a": [ { "c": [true, "s", 1.5], "b": null } ] }""")]
    [InlineData("""{"a":[{"b":null,"c":[true,"s",1.5]}]}""", """{ "a": [ { "c": [true, "s", 2.5], "b": null } ] }""")]
    public void ElementsAreComparedAsDotNetComparesThem(string actual, string expected)
    {
        if (JsonElement.DeepEquals(Element(actual), Element(expected)))
        {
            Element(actual).ShouldBe(Element(expected));
            Element(expected).ShouldBe(Element(actual));
        }
        else
        {
            Element(actual).ShouldNotBe(Element(expected));
            Element(expected).ShouldNotBe(Element(actual));
        }
    }

    // An element may repeat a property name, which a node can't hold.
    [Fact]
    public void ElementsThatRepeatAPropertyNameAreCompared()
    {
        Element("""{"x":1,"x":2}""").ShouldBe(Element("""{ "x": 1, "x": 2 }"""));
        Element("""{"x":1,"x":2}""").ShouldNotBe(Element("""{"x":1,"x":3}"""));
        Element("""{"x":1,"x":2}""").ShouldNotBe(Element("""{"x":1}"""));
        Should.Throw<ShouldAssertException>(() => Element("""{"x":1,"x":2}""").ShouldBe(Element("""{"x":1,"x":3}""")))
            .Message.ShouldContain("""{"x":1,"x":3}""");
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
