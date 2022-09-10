namespace Shouldly.Tests.Dictionaries;

public class ShouldNotContainKey
{
    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _classDictionary.ShouldNotContainKey(ThingKey, "Some additional context"),

            errorWithSource:
            @"_classDictionary
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _guidDictionary.ShouldNotContainKey(GuidKey, "Some additional context"),

            errorWithSource:
            @"_guidDictionary
    should not contain key
89bdbe3d-3436-4749-bcb7-84264394026c
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[89bdbe3d-3436-4749-bcb7-84264394026c => 96408719-fdd4-4212-8e54-4f4d7371300f]]
    should not contain key
89bdbe3d-3436-4749-bcb7-84264394026c
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void StringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _stringDictionary.ShouldNotContainKey("Foo", "Some additional context"),

            errorWithSource:
            @"_stringDictionary
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[""Foo"" => """"]]
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        _classDictionary.ShouldNotContainKey(new());
        _guidDictionary.ShouldNotContainKey(Guid.NewGuid());
        _stringDictionary.ShouldNotContainKey("bar");
    }

    private readonly Dictionary<MyThing, MyThing> _classDictionary = new()
    {
        { ThingKey, new() }
    };
    private readonly Dictionary<Guid, Guid> _guidDictionary = new()
    {
        { GuidKey, new("96408719-fdd4-4212-8e54-4f4d7371300f") }
    };
    private readonly Dictionary<string, string> _stringDictionary = new()
    {
        { "Foo", "" }
    };

    private static readonly Guid GuidKey = new("89bdbe3d-3436-4749-bcb7-84264394026c");
    private static readonly MyThing ThingKey = new();
}