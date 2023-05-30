using System.Collections.ObjectModel;

namespace Shouldly.Tests.Dictionaries;

public class ShouldNotContainKey
{
    [Theory]
    [MemberData(nameof(ClassDictionaries))]
    private void ShouldNotContainKeyClassScenarioShouldFail(IEnumerable<KeyValuePair<MyThing, MyThing>> classDictionary)
    {
        Verify.ShouldFail(() =>
                classDictionary.ShouldNotContainKey(ThingKey, "Some additional context"),

            errorWithSource:
            @"classDictionary
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

    [Theory]
    [MemberData(nameof(GuidDictionaries))]
    public void ShouldNotContainKeyGuidScenarioShouldFail(IEnumerable<KeyValuePair<Guid, Guid>> guidDictionary)
    {
        Verify.ShouldFail(() =>
                guidDictionary.ShouldNotContainKey(GuidKey, "Some additional context"),

            errorWithSource:
            @"guidDictionary
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

    [Theory]
    [MemberData(nameof(StringDictionaries))]
    public void StringScenarioShouldFail(IEnumerable<KeyValuePair<string, string>> stringDictionary)
    {
        Verify.ShouldFail(() =>
                stringDictionary.ShouldNotContainKey("Foo", "Some additional context"),

            errorWithSource:
            @"stringDictionary
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
        foreach (var classDictionary in ClassDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<MyThing, MyThing>>>())
        {
            classDictionary.ShouldNotContainKey(new MyThing());
        }

        foreach (var guidDictionary in GuidDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<Guid, Guid>>>())
        {
            guidDictionary.ShouldNotContainKey(Guid.NewGuid());
        }

        foreach (var stringDictionary in StringDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<string, string>>>())
        {
            stringDictionary.ShouldNotContainKey("bar");
        }
    }

    private static readonly MyThing ThingKey = new MyThing();
    private static readonly Dictionary<MyThing, MyThing> _classDictionary = new Dictionary<MyThing, MyThing>
    {
        { ThingKey, new() }
    };

    private static readonly Guid GuidKey = new Guid("89bdbe3d-3436-4749-bcb7-84264394026c");
    private static readonly Dictionary<Guid, Guid> _guidDictionary = new Dictionary<Guid, Guid>
    {
        { GuidKey, new("96408719-fdd4-4212-8e54-4f4d7371300f") }
    };
    private static readonly Dictionary<string, string> _stringDictionary = new Dictionary<string, string>
    {
        { "Foo", "" }
    };

    public static IEnumerable<object[]> ClassDictionaries()
    {
        yield return new[] { _classDictionary };
        yield return new[] { new ReadOnlyDictionary<MyThing, MyThing>(_classDictionary) };
        yield return new[] { _classDictionary.ToArray() };
    }

    public static IEnumerable<object[]> StringDictionaries()
    {
        yield return new[] { _stringDictionary };
        yield return new[] { new ReadOnlyDictionary<string, string>(_stringDictionary) };
        yield return new[] { _stringDictionary.ToArray() };
    }

    public static IEnumerable<object[]> GuidDictionaries()
    {
        yield return new[] { _guidDictionary };
        yield return new[] { new ReadOnlyDictionary<Guid, Guid>(_guidDictionary) };
        yield return new[] { _guidDictionary.ToArray() };
    }
}